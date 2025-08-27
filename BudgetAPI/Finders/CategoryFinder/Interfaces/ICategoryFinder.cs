using BudgetAPI.DTOs.Projection;

namespace BudgetAPI.Finders.CategoryFinder.Interfaces;

public interface ICategoryFinder
{
	Task<List<CategoryProjection>> GetAll();
}