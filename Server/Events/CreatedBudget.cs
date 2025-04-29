
namespace Server.Events;

public class CreatedBudget : EventEntity
{
	public required string BudgetName { get; set; }
}