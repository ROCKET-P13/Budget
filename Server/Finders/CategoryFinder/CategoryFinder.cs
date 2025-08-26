using Microsoft.EntityFrameworkCore;
using Budget.Data;
using Budget.DTOs.Projection;
using Budget.Finders.CategoryFinder.Interfaces;

namespace Budget.Finders.CategoryFinder;

public class CategoryFinder(AppDatabaseContext databaseContext) : ICategoryFinder
{
	private readonly AppDatabaseContext _dbContext = databaseContext;

	public async Task<List<CategoryProjection>> GetAll ()
	{
		return await _dbContext.CategoryProjections.ToListAsync();
	}	
}