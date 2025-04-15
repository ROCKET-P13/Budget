using Server.Models;

namespace Server.Repositories.BudgetRepository.Interfaces
{
	public interface IBudgetRepository
	{
		Task<Budget> Create(Budget budget);
		Task<Budget?> GetBudgetById(int id);
	}
}