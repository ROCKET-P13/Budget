namespace Server.DTOs;

public class CreateCategoryRequest
{
	public Guid BudgetId { get; set; }
	public string Name { get; set; } = string.Empty;
	public decimal SpendingLimit { get; set; }
}