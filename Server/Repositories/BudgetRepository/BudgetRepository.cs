using Microsoft.EntityFrameworkCore;
using Server.Data;
using Server.Models;
using Server.Repositories.BudgetRepository.Interfaces;

namespace Server.Repositories.BudgetRepository
{
	public class BudgetRepository(AppDatabaseContext context): IBudgetRepository
	{
		private readonly AppDatabaseContext _context = context;

		public async Task<Budget> Create(Budget budget)
		{
			_context.Budgets.Add(budget);
			await _context.SaveChangesAsync();
			return budget;
		}

		public async Task<Budget?> GetBudgetById(int id)
		{
			var budget = await _context.Budgets
				.Include(b => b.Categories)
					.ThenInclude(c => c.Transactions)
				.FirstOrDefaultAsync(b => b.Id == id);
				
			return budget;
		}
	}
}

