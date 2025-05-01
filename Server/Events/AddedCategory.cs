namespace Server.Events;

public class AddedCategory : EventEntity
{
	public Guid CategoryId { get; set; }
	public string CategoryName { get; set; } = string.Empty;
	public decimal PlannedAmount { get; set; }
	public bool? IsDebt { get; set; } = false;
}