namespace Server.DTOs.Requests;

public class UpdateCategoryRequest
{
	public Guid BudgetId { get; set; }
	public Guid CategoryId { get; set; }
	public string Name { get; set; } = string.Empty;
	public decimal SpendingLimit { get; set; }
}