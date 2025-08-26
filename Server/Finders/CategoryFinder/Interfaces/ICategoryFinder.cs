using Budget.DTOs.Projection;

namespace Budget.Finders.CategoryFinder.Interfaces;

public interface ICategoryFinder
{
	Task<List<CategoryProjection>> GetAll();
}