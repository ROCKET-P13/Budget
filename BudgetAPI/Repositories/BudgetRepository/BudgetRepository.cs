using Microsoft.EntityFrameworkCore;
using BudgetAPI.Aggregates;
using BudgetAPI.Data;
using BudgetAPI.Data.Interfaces;
using BudgetAPI.DTOs.Projection;
using BudgetAPI.Repositories.BudgetRepository.Interfaces;

namespace BudgetAPI.Repositories.BudgetRepository;

public class BudgetRepository(IEventStore eventStore, AppDatabaseContext databaseContext) : IBudgetRepository
{
	private readonly IEventStore _eventStore = eventStore;
	private readonly AppDatabaseContext _dbContext = databaseContext;
	public async Task<Budget> GetById(Guid budgetId)
	{
		var events = await _eventStore.GetBudgetEvents(budgetId);
		if (events.Count == 0)
			throw new InvalidOperationException("Budget not found");
		
		return new Budget(events);

	}

	public async Task SaveAsync(Budget budget)
	{
		await _eventStore.SaveBudgetEvents(budget.Id, budget.GetUncommittedChanges());
		budget.MarkChangesAsCommitted();

		var existingProjection = _dbContext.BudgetProjections.AsNoTracking().FirstOrDefault(p => p.Id == budget.Id);

		if (existingProjection != null)
		{
			_dbContext.BudgetProjections.Update(
				new BudgetProjection
				{
					Id = budget.Id,
					Name = budget.Name,
				}
			);
		} else
		{
			_dbContext.BudgetProjections.Add(
				new BudgetProjection
				{
					Id = budget.Id,
					Name = budget.Name,
				}
			);
		}

		await _dbContext.SaveChangesAsync();

	}
}