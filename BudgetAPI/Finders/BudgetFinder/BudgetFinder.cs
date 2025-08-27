using Microsoft.EntityFrameworkCore;
using BudgetAPI.Data;
using BudgetAPI.DTOs.Projection;
using BudgetAPI.Finders.BudgetFinder.Interfaces;

namespace BudgetAPI.Finders.BudgetFinder;

public class BudgetFinder(AppDatabaseContext databaseContext) : IBudgetFinder
{
	private readonly AppDatabaseContext _dbContext = databaseContext;

	public async Task<List<BudgetProjection>> GetAll ()
	{
		return await _dbContext.BudgetProjections.ToListAsync();
	}
}