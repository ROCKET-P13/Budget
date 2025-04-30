namespace Server.Models;

public class Transaction
{
	public Guid Id { get; set; }
	public string? Date { get; set; }
	public decimal Amount { get; set; }
	public string? Merchant { get; set; }
	public string? Description { get; set; }
	public Guid? CategoryId { get; set; }
}