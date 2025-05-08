using Server.Aggregates;

namespace Server.Factories.CategoryFactory.Interfaces;

public interface ICategoryFactory
{
	public CategoryAggregate Create(string name);
}