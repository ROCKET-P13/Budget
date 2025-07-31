namespace DTOs.Requests;

public class AddCategoryToBudgetRequest
{
	public Guid CategoryId { get; set; }
	public decimal PlannedAmount { get; set; }
}