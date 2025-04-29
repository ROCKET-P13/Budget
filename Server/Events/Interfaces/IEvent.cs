namespace Server.Events.Interfaces;

public interface IEvent
{
	public Guid Id {get; }
	public Guid BudgetId { get; }
	public DateTime Timestamp { get; }
}