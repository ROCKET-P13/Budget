using Server.Aggregates;

namespace Server.Factories.CategoryFactory.Interfaces;

public interface ICategoryFactory
{
	public Category Create(string name, bool isDebt);
}