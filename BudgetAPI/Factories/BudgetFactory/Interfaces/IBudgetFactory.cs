using BudgetAPI.Aggregates;

namespace BudgetAPI.Factories.BudgetFactory.Interfaces;

public interface IBudgetFactory
{
	public Budget Create(string name);
}