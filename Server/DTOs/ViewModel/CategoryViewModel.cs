namespace Server.DTOs.ViewModel;

public class CategoryViewModel
{
	public Guid Id { get; set; }
	public string? Name { get; set; }
	public decimal? PlannedAmount { get; set; }

	public List<TransactionViewModel> Transactions { get; set; } = [];
}