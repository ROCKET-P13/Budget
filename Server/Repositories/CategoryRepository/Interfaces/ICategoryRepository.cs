using Budget.Aggregates;

namespace Budget.Repositories.CategoryRepository.Interfaces;
public interface ICategoryRepository
{
	Task SaveAsync(Category category);
	Task<Category> GetById(Guid categoryId);
}