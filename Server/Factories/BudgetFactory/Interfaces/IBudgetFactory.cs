using Server.Aggregates;

namespace Server.Factories.BudgetFactory.Interfaces;

public interface IBudgetFactory
{
	public Budget Create(string name, int month, int year);
}