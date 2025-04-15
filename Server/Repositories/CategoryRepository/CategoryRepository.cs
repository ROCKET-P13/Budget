using Server.Data;
using Server.Models;
using Server.Repositories.CategoryRepository.Interfaces;

namespace Server.Repositories.CategoryRepository
{
	public class CategoryRepository(AppDatabaseContext context): ICategoryRepository
	{
		private readonly AppDatabaseContext _context = context;
		public async Task<Category> Create(Category category)
		{
			_context.Categories.Add(category);
			await _context.SaveChangesAsync();
			return category;
		}
	}
}