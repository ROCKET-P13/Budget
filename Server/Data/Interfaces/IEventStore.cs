using Server.Events;

namespace Server.Data.Interfaces;

public interface IEventStore
{
	Task SaveEventsAsync(Guid budgetId, IReadOnlyCollection<EventEntity> events);
	Task<List<EventEntity>> GetEventsAsync (Guid budgetId);
}