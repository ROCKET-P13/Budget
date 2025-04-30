namespace Server.DTOs;

public class CategoryDTO
{
	public Guid Id { get; set; }
	public string Name { get; set; } = string.Empty;
	public decimal SpendingLimit { get; set; }

	public List<TransactionDTO> Transactions { get; set; } = [];
}