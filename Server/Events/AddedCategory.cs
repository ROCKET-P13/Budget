namespace Server.Events;

public class AddedCategory : EventEntity
{
	public new Guid Id { get; }
	public new Guid BudgetId { get; set; }
	public new DateTime Timestamp { get; }
	public Guid CategoryId { get; set; }
	public string CategoryName { get; set; } = string.Empty;
}