namespace Server.DTOs;

public class TransactionDTO
{
	public int Id { get; set; }
	public string Merchant { get; set; } = string.Empty;
	public decimal Amount { get; set; }
	public DateTime Date { get; set; }

	public int CategoryId { get; set; }
	public CategoryDTO? Category { get; set; }
}