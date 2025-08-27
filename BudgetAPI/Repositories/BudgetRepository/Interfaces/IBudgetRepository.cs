using BudgetAPI.Aggregates;

namespace BudgetAPI.Repositories.BudgetRepository.Interfaces;
public interface IBudgetRepository
{
	Task<Budget> GetById(Guid id);
	Task SaveAsync(Budget budget);
}