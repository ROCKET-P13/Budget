namespace Server.DTOs.Projection;

public class CategoryProjection
{
	public Guid Id { get; set; }
	public required string Name { get; set; }
	public bool IsDebt { get; set; }
	public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}