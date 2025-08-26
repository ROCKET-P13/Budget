using Budget.DTOs.Projection;

namespace Budget.Finders.BudgetFinder.Interfaces;

public interface IBudgetFinder
{
	Task<List<BudgetProjection>> GetAll();
}