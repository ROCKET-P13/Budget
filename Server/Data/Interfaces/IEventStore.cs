using Budget.Events;
using Budget.Events.Budget;
using Budget.Events.Category;

namespace Budget.Data.Interfaces;

public interface IEventStore
{
	Task SaveBudgetEvents(Guid budgetId, IReadOnlyCollection<EventEntity> events);
	Task SaveCategoryEvents(Guid categoryId, IReadOnlyCollection<EventEntity> events);
	Task<List<BudgetEventEntity>> GetBudgetEvents (Guid budgetId);
	Task<List<CategoryEventEntity>> GetCategoryEvents (Guid categoryId);

}