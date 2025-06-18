namespace Server.DTOs.ViewModel;

public class BudgetViewModel
{
	public Guid Id { get; set; }
	public string Name { get; set; } = string.Empty;
	public int Month { get; set; }
	public int Year { get; set; }
	public decimal TotalPlannedAmount { get; set; }
	public List<CategoryViewModel> Categories { get; set; } = [];
}