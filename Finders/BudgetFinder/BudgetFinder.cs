using Microsoft.EntityFrameworkCore;
using Server.Data;
using Server.DTOs.Projection;
using Server.Finders.BudgetFinder.Interfaces;

namespace Server.Finders.BudgetFinder;

public class BudgetFinder(AppDatabaseContext databaseContext) : IBudgetFinder
{
	private readonly AppDatabaseContext _dbContext = databaseContext;

	public async Task<List<BudgetProjection>> GetAll ()
	{
		return await _dbContext.BudgetProjections.ToListAsync();
	}
}