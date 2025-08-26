using Budget.Events.Budget;
using Budget.Factories.BudgetFactory.Interfaces;
using Budget.Aggregates;

namespace Budget.Factories.BudgetFactory;

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