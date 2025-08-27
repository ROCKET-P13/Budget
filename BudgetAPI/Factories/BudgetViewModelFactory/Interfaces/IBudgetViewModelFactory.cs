using BudgetAPI.Aggregates;
using BudgetAPI.DTOs.ViewModel;

namespace BudgetAPI.Factories.BudgetViewModelFactory.Interfaces;

public interface IBudgetViewModelFactory
{
	public BudgetViewModel FromAggregate(Budget budget);
}