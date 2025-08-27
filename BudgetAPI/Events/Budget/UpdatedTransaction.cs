namespace BudgetAPI.Events.Budget;

public class UpdatedTransaction : BudgetEventEntity
{
	public Guid TransactionId { get; set; }
	public Guid? CategoryId { get; set; }
	public decimal Amount { get; set; }
	public string? Description { get; set; }
	public string? Merchant { get; set; }
	public string? Date { get; set; }
}