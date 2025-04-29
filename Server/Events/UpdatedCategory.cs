namespace Server.Events;

public class UpdatedCategory : EventEntity
{
	public Guid CategoryId { get; set; }
	public decimal SpendingLimit { get; set; }
	public string CategoryName { get; set; } = string.Empty;
}