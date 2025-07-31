namespace Server.DTOs.Requests;

public class AddTransactionToBudgetRequest
{
	public decimal Amount { get; set; }
	public string? Description { get; set; }
	public string? Date { get; set; }
	public Guid? CategoryId { get; set; }
	public string? Merchant { get; set; }
}