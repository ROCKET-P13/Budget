using Server.Events.Budget;
using Server.Factories.BudgetFactory.Interfaces;
using Server.Aggregates;

namespace Server.Factories.BudgetFactory;

public class BudgetFactory : IBudgetFactory
{
	public Budget Create(string name, int month, int year)
	{
		var budgetCreatedEvent = new CreatedBudget
		{
			BudgetName = name,
			BudgetId = Guid.NewGuid(),
			CreatedAt = DateTime.UtcNow,
			Month = month,
			Year = year
		};

		var budget = new Budget([budgetCreatedEvent]);

		return budget;
	}
}