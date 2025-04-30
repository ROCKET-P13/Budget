namespace Server.Events;

public class AddedTransaction : EventEntity
{
	public new Guid BudgetId { get; set; } 
	public Guid? CategoryId { get; set; }
	public Guid TransactionId { get; set; }
	public decimal Amount { get; set; }
	public string? Description { get; set; }
	public string? Merchant { get; set; }
	public string? Date { get; set; } 
}