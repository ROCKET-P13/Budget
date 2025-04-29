namespace Server.Services.BudgetService.Interfaces;

public interface IBudgetService
{
	Task<Guid> CreateBudget(string name);
	Task AddTransaction(
		Guid budgetId,
		decimal amount,
		string description,
		DateTime date,
		Guid categoryId
	);
}