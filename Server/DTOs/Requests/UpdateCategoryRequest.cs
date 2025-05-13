namespace Server.DTOs.Requests;

public class UpdateCategoryRequest
{
	public string Name { get; set; } = string.Empty;
	public decimal PlannedAmount { get; set; }
}