using Aggregates;

namespace Factories.CategoryFactory.Interfaces;

public interface ICategoryFactory
{
	public Category Create(string name, bool isDebt);
}