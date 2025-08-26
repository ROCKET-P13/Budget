namespace Budget.Events.Budget;

public class BudgetEvent : Event
{
	public Guid BudgetId { get; set; }
}