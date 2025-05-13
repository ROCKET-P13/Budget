using Server.Aggregates;
using Server.Data.Interfaces;
using Server.Repositories.BudgetRepository.Interfaces;

namespace Server.Repositories.BudgetRepository;

public class BudgetRepository(IEventStore eventStore) : IBudgetRepository
{
	private readonly IEventStore _eventStore = eventStore;

	public async Task<Budget> GetById(Guid budgetId)
	{
		var events = await _eventStore.GetBudgetEvents(budgetId);
		if (events.Count == 0)
			throw new InvalidOperationException("Budget not found");
		
		return new Budget(events);

	}

	public async Task SaveAsync(Budget budget)
	{
		var newEvents = budget.GetUncommittedChanges();
		await _eventStore.SaveBudgetEvents(budget.Id, newEvents);
		budget.MarkChangesAsCommitted();
	}
}