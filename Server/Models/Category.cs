namespace Server.Models;

public class Category
{
	public Guid Id { get; set; }
	public int BudgetId { get; set; }
	public Budget? Budget { get; set; }
	public string Name { get; set; } = string.Empty;
	public decimal Amount { get; set; }
	public IList<Transaction> Transactions { get; set; } = [];

}