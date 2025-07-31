using DTOs.Projection;

namespace Finders.BudgetFinder.Interfaces;

public interface IBudgetFinder
{
	Task<List<BudgetProjection>> GetAll();
}