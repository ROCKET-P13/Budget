namespace Budget.Events.Budget;

public class AddedTransaction : BudgetEventEntity
{
	public Guid? CategoryId { get; set; }
	public Guid TransactionId { get; set; }
	public decimal Amount { get; set; }
	public string? Description { get; set; }
	public string? Merchant { get; set; }
	public string? Date { get; set; } 
}