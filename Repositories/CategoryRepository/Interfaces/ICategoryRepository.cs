using Aggregates;

namespace Repositories.CategoryRepository.Interfaces;
public interface ICategoryRepository
{
	Task SaveAsync(Category category);
	Task<Category> GetById(Guid categoryId);
}