namespace Server.DTOs;

public class BudgetDTO
{
	public int Id { get; set; }
	public string Name { get; set; } = string.Empty;
	public DateTime CreatedAt { get; set; }

	public List<CategoryDTO> Categories { get; set; } = [];
}