namespace Budget.Events.Category;

public class UpdatedCategoryName : CategoryEventEntity
{
	public required string Name { get; set; }
}