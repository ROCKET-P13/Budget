using Server.Factories.BudgetViewModelFactory.Interfaces;
using Server.DTOs.ViewModel;
using Server.Aggregates;

namespace Server.Factories.BudgetViewModelFactory;

public class BudgetViewModelFactory : IBudgetViewModelFactory
{
	public BudgetViewModel FromAggregate(Budget budget)
	{
		var budgetDTO = new BudgetViewModel
		{
			Id = budget.Id,
			Name = budget.Name,
			TotalPlannedAmount = budget.Categories.Sum(c => c.PlannedAmount),
			Categories = [
				.. budget.Categories.Select(c => new CategoryViewModel
				{
					Id = c.Id,
					Name = c.Name,
					PlannedAmount = c.PlannedAmount,
					IsDebt = c.IsDebt,
					SpentAmount = budget.Transactions.Sum(t => t.Amount),
					Transactions = [
						.. budget.Transactions
						.Where(t => t.CategoryId == c.Id)
						.Select(t => new TransactionViewModel
						{
							Id = t.Id,
							Description = t.Description,
							Merchant = t.Merchant,
							Amount = t.Amount,
							Date = t.Date
						}).ToList()
					]
				}).ToList()
			]
		};

		return budgetDTO;
	}

}