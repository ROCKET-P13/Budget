namespace Server.DTOs;

public class CategoryDTO
{
	public Guid Id { get; set; }
	public string Name { get; set; } = string.Empty;
	public decimal Amount { get; set; }

	public ICollection<TransactionDTO> Transactions { get; set; } = [];
}