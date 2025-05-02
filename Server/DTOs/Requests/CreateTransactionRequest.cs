namespace Server.DTOs.Requests;

public class CreateTransactionRequest
{
	public required Guid BudgetId { get; set; }
	public string? Merchant { get; set; }
	public decimal Amount { get; set; }
	public string Date { get; set; } = string.Empty;
	public Guid? CategoryId { get; set; }
	public string? Description { get; set; }
}