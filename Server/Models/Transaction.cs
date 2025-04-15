namespace Server.Models;

public class Transaction
{
	public int Id { get; set; }
	public DateTime Date { get; set; } = DateTime.UtcNow;
	public required decimal Amount { get; set; }
	public string Merchant { get; set; } = string.Empty;
	public int CategoryId { get; set; }
	public Category? Category { get; set; }
}