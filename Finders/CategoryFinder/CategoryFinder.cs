using Microsoft.EntityFrameworkCore;
using Data;
using DTOs.Projection;
using Finders.CategoryFinder.Interfaces;

namespace Finders.CategoryFinder;

public class CategoryFinder(AppDatabaseContext databaseContext) : ICategoryFinder
{
	private readonly AppDatabaseContext _dbContext = databaseContext;

	public async Task<List<CategoryProjection>> GetAll ()
	{
		return await _dbContext.CategoryProjections.ToListAsync();
	}	
}