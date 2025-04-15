using Server.Models;

namespace Server.Repositories.CategoryRepository.Interfaces
{
	public interface ICategoryRepository
	{
		Task <Category> Create(Category category);
	}
}