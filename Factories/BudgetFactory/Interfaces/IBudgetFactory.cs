using Aggregates;

namespace Factories.BudgetFactory.Interfaces;

public interface IBudgetFactory
{
	public Budget Create(string name);
}