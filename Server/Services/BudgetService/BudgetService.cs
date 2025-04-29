using Server.Factories.BudgetFactory.Interfaces;
using Server.Repositories.BudgetRepository.Interfaces;

namespace Server.Services.BudgetService;

public class BudgetService(IBudgetRepository budgetRepository, IBudgetFactory budgetFactory)
{
	private readonly IBudgetRepository _budgetRepository = budgetRepository;
	private readonly IBudgetFactory _budgetFactory = budgetFactory;
	// public async Task<Guid> CreateBudget(string name)
	// {
	// 	var budget = _budgetFactory.Create(name);
		
	// 	// await _budgetRepository
	// }
}