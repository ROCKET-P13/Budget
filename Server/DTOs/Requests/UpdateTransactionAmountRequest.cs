namespace Server.DTOs.Requests;

public class UpdateTransactionRequest
{
	public Guid BudgetId { get; set; }
	public Guid CategoryId { get; set; }
	public decimal Amount { get; set; }
	public string? Description { get; set; }

}