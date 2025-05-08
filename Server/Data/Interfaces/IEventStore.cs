using Server.Events;
using Server.Events.Budget;
using Server.Events.Category;

namespace Server.Data.Interfaces;

public interface IEventStore
{
	Task SaveBudgetEventsAsync(Guid budgetId, IReadOnlyCollection<EventEntity> events);
	Task SaveCategoryEventsAsync(Guid categoryId, IReadOnlyCollection<EventEntity> events);
	Task<List<BudgetEventEntity>> GetBudgetEventsAsync (Guid budgetId);
	Task<List<CategoryEventEntity>> GetCategoryEventsAsync (Guid categoryId);

}