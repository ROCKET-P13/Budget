namespace DTOs.ViewModel;

public class BudgetViewModel
{
	public Guid Id { get; set; }
	public string Name { get; set; } = string.Empty;
	public decimal TotalPlannedAmount { get; set; }
	public List<CategoryViewModel> Categories { get; set; } = [];
}