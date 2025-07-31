using Aggregates;
using DTOs.ViewModel;

namespace Factories.BudgetViewModelFactory.Interfaces;

public interface IBudgetViewModelFactory
{
	public BudgetViewModel FromAggregate(Budget budget);
}