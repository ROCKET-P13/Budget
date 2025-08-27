using BudgetAPI.Events.Budget;
using BudgetAPI.Factories.BudgetFactory.Interfaces;
using BudgetAPI.Aggregates;

namespace BudgetAPI.Factories.BudgetFactory;

public class BudgetFactory : IBudgetFactory
{
	public Budget Create(string name)
	{
		var budgetCreatedEvent = new CreatedBudget
		{
			BudgetName = name,
			BudgetId = Guid.NewGuid()
		};

		var budget = new Budget([budgetCreatedEvent]);

		return budget;
	}
}