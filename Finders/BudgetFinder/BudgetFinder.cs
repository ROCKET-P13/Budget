using Microsoft.EntityFrameworkCore;
using Data;
using DTOs.Projection;
using Finders.BudgetFinder.Interfaces;

namespace Finders.BudgetFinder;

public class BudgetFinder(AppDatabaseContext databaseContext) : IBudgetFinder
{
	private readonly AppDatabaseContext _dbContext = databaseContext;

	public async Task<List<BudgetProjection>> GetAll ()
	{
		return await _dbContext.BudgetProjections.ToListAsync();
	}
}