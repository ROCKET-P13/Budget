namespace Server.DTOs;

public class BudgetDTO
{
	public Guid Id { get; set; }
	public string Name { get; set; } = string.Empty;
	public List<CategoryDTO> Categories { get; set; } = [];
}