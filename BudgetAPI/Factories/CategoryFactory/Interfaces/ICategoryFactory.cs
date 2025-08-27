using BudgetAPI.Aggregates;

namespace BudgetAPI.Factories.CategoryFactory.Interfaces;

public interface ICategoryFactory
{
	public Category Create(string name, bool isDebt);
}