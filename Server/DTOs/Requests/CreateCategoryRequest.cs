namespace Server.DTOs.Requests;

public class CreateCategoryRequest
{
	public Guid BudgetId { get; set; }
	public string Name { get; set; } = string.Empty;
}