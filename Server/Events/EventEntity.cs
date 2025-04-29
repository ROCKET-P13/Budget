namespace Server.Events;

public class EventEntity
{
	public Guid Id { get; set; } = Guid.Empty;
	public DateTime Timestamp { get; set; }
	public Guid BudgetId { get; set; }
}