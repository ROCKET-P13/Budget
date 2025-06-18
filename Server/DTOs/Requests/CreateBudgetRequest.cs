namespace Server.DTOs.Requests;

public class CreateBudgetRequest
{
	public required string Name { get; set; }
	public int Month { get; set; }
	public int Year { get; set; }
}