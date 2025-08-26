
namespace Budget.Events.Category;

public class CreatedCategory : CategoryEventEntity
{
	public required string CategoryName { get; set; }
	public bool IsDebt { get; set; }
	public decimal? PlannedAmount { get; set; }
	public DateTime CreatedAt { get; set; }
}