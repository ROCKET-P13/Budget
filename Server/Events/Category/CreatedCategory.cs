
namespace Server.Events.Category;

public class CreatedCategory : CategoryEventEntity
{
	public required string CategoryName { get; set; }
}