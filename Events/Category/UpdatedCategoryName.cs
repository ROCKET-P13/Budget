namespace Server.Events.Category;

public class UpdatedCategoryName : CategoryEventEntity
{
	public required string Name { get; set; }
}