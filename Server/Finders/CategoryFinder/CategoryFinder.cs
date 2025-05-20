using Microsoft.EntityFrameworkCore;
using Server.Data;
using Server.DTOs.Projection;
using Server.Finders.CategoryFinder.Interfaces;

namespace Server.Finders.CategoryFinder;

public class CategoryFinder(AppDatabaseContext databaseContext) : ICategoryFinder
{
	private readonly AppDatabaseContext _dbContext = databaseContext;

	public async Task<List<CategoryProjection>> GetAll ()
	{
		return await _dbContext.CategoryProjections.ToListAsync();
	}	
}