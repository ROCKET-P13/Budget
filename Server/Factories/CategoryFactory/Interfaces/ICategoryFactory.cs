using Budget.Aggregates;

namespace Budget.Factories.CategoryFactory.Interfaces;

public interface ICategoryFactory
{
	public Category Create(string name, bool isDebt);
}