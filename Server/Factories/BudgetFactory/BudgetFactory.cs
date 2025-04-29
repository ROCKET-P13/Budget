using Server.Events;
using Server.Factories.BudgetFactory.Interfaces;
using Server.Aggregates;

namespace Server.Factories.BudgetFactory;

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