using Microsoft.EntityFrameworkCore;
using Budget.Data;
using Budget.DTOs.Projection;
using Budget.Finders.BudgetFinder.Interfaces;

namespace Budget.Finders.BudgetFinder;

public class BudgetFinder(AppDatabaseContext databaseContext) : IBudgetFinder
{
	private readonly AppDatabaseContext _dbContext = databaseContext;

	public async Task<List<BudgetProjection>> GetAll ()
	{
		return await _dbContext.BudgetProjections.ToListAsync();
	}
}