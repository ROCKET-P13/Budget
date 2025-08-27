using BudgetAPI.DTOs.Projection;

namespace BudgetAPI.Finders.BudgetFinder.Interfaces;

public interface IBudgetFinder
{
	Task<List<BudgetProjection>> GetAll();
}