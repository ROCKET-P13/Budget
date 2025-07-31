
namespace Events.Budget;

public class CreatedBudget : BudgetEventEntity
{
	public required string BudgetName { get; set; }
}