using Microsoft.EntityFrameworkCore;
using Server.Aggregates;
using Server.Data;
using Server.Data.Interfaces;
using Server.DTOs.Projection;
using Server.Repositories.CategoryRepository.Interfaces;

namespace Server.Repositories.CategoryRepository;

public class CategoryRepository(IEventStore eventStore, AppDatabaseContext databaseContext) : ICategoryRepository
{
	private readonly IEventStore _eventStore = eventStore;
	private readonly AppDatabaseContext _dbContext = databaseContext;

    public async Task<Category> GetById(Guid categoryId)
	{
		var events = await _eventStore.GetCategoryEvents(categoryId);
		if (events.Count == 0)
			throw new InvalidOperationException("Category not found");

		return new Category(events);
	}

	public async Task SaveAsync(Category category)
	{
		await _eventStore.SaveCategoryEvents(category.Id, category.GetUncommittedChanges());
		category.MarkChangesAsCommitted();

		var existingProjection = _dbContext.CategoryProjections.AsNoTracking().FirstOrDefault(p => p.Id == category.Id);

		if (existingProjection != null)
		{
			_dbContext.CategoryProjections.Update(
				new CategoryProjection
				{
					Id = category.Id,
					Name = category.Name,
					IsDebt = category.IsDebt,
					CreatedAt = category.CreatedAt
				}
			);
		} else
		{
			_dbContext.CategoryProjections.Add(
				new CategoryProjection
				{
					Id = category.Id,
					Name = category.Name,
					IsDebt = category.IsDebt,
					CreatedAt = category.CreatedAt
				}
			);
		}


		await _dbContext.SaveChangesAsync();
	}
}