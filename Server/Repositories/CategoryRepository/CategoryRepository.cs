using Server.Aggregates;
using Server.Data.Interfaces;
using Server.Repositories.CategoryRepository.Interfaces;

namespace Server.Repositories.CategoryRepository;

public class CategoryRepository(IEventStore eventStore) : ICategoryRepository
{
	private readonly IEventStore _eventStore = eventStore;

	public async Task<Category> GetById(Guid categoryId)
	{
		var events = await _eventStore.GetCategoryEventsAsync(categoryId);
		if (events.Count == 0)
			throw new InvalidOperationException("Category not found");

		return new Category(events);
	}

	public async Task SaveAsync(Category category)
	{
		var newEvents = category.GetUncommittedChanges();
		await _eventStore.SaveCategoryEventsAsync(category.Id, newEvents);
		category.MarkChangesAsCommitted();
	}
}