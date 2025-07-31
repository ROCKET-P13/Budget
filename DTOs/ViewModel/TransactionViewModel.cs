namespace DTOs.ViewModel;

public class TransactionViewModel
{
	public Guid Id { get; set; }
	public string? Merchant { get; set; }
	public decimal? Amount { get; set; }
	public string? Date { get; set; }
	public string? Description { get; set; }
}