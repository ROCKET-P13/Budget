using Budget.Aggregates;

namespace Budget.Repositories.BudgetRepository.Interfaces;
public interface IBudgetRepository
{
	Task<Budget> GetById(Guid id);
	Task SaveAsync(Budget budget);
}