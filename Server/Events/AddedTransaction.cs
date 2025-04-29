namespace Server.Events;

public class AddedTransaction : EventEntity
{
	public new Guid Id { get; } = Guid.NewGuid();
	public new Guid BudgetId { get; } 
	public new DateTime Timestamp { get; } = DateTime.UtcNow;

	public Guid CategoryId { get; set; }
	public Guid TransactionId { get; set; }
	public decimal Amount { get; set; }
	public string Description { get; set; } = string.Empty;
	public DateTime Date { get; set; } 
}