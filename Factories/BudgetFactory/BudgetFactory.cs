using Events.Budget;
using Factories.BudgetFactory.Interfaces;
using Aggregates;

namespace Factories.BudgetFactory;

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