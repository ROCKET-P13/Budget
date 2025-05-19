using Server.DTOs.Projection;

namespace Server.Finders.CategoryFinder.Interfaces;

public interface ICategoryFinder
{
	Task<List<CategoryProjection>> GetAll();
}