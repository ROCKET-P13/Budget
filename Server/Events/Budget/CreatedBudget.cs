
namespace Server.Events.Budget;

public class CreatedBudget : BudgetEventEntity
{
	public required string BudgetName { get; set; }
	public required DateTime CreatedAt { get; set; }
}