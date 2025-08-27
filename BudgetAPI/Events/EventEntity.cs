namespace BudgetAPI.Events;

public class EventEntity
{
	public Guid Id { get; set; } = Guid.Empty;
	public DateTime Timestamp { get; set; }
}