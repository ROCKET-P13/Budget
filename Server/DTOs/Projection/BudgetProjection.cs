namespace Server.DTOs.Projection;

public class BudgetProjection
{
	public Guid Id { get; set; }
	public required string Name { get; set; }
	public int Month { get; set; }
	public int Year { get; set; }
	public DateTime CreatedAt { get; set; }
}