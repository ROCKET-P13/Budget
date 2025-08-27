using Microsoft.EntityFrameworkCore;
using BudgetAPI.Data;
using BudgetAPI.DTOs.Projection;
using BudgetAPI.Finders.CategoryFinder.Interfaces;

namespace BudgetAPI.Finders.CategoryFinder;

public class CategoryFinder(AppDatabaseContext databaseContext) : ICategoryFinder
{
	private readonly AppDatabaseContext _dbContext = databaseContext;

	public async Task<List<CategoryProjection>> GetAll ()
	{
		return await _dbContext.CategoryProjections.ToListAsync();
	}	
}