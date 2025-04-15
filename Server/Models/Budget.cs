namespace Server.Models;

public class Budget
{
	public int Id { get; set; }
	public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
	public string Name { get; set; } = string.Empty;
	public string Month { get; set; } = string.Empty;
	public string Year { get; set; } = string.Empty;
	public ICollection<Category> Categories { get; set; } = [];
}