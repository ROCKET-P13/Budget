using Budget.Aggregates;

namespace Budget.Factories.BudgetFactory.Interfaces;

public interface IBudgetFactory
{
	public Budget Create(string name);
}