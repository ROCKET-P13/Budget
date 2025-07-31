using DTOs.Projection;

namespace Finders.CategoryFinder.Interfaces;

public interface ICategoryFinder
{
	Task<List<CategoryProjection>> GetAll();
}