using Server.Aggregates;

namespace Server.Repositories.CategoryRepository.Interfaces;
public interface ICategoryRepository
{
	Task SaveAsync(CategoryAggregate category);
}