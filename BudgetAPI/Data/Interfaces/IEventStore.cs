using BudgetAPI.Events;
using BudgetAPI.Events.Budget;
using BudgetAPI.Events.Category;

namespace BudgetAPI.Data.Interfaces;

public interface IEventStore
{
	Task SaveBudgetEvents(Guid budgetId, IReadOnlyCollection<EventEntity> events);
	Task SaveCategoryEvents(Guid categoryId, IReadOnlyCollection<EventEntity> events);
	Task<List<BudgetEventEntity>> GetBudgetEvents (Guid budgetId);
	Task<List<CategoryEventEntity>> GetCategoryEvents (Guid categoryId);

}