namespace Server.Events.Budget;

public class AddedCategory : BudgetEventEntity
{
	public Guid CategoryId { get; set; }
	public string CategoryName { get; set; } = string.Empty;
	public decimal PlannedAmount { get; set; }
	public bool? IsDebt { get; set; } = false;
}