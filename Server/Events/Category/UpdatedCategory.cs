namespace Server.Events.Category;

public class UpdatedCategory : CategoryEventEntity
{
	public decimal PlannedAmount { get; set; }
	public string CategoryName { get; set; } = string.Empty;
}