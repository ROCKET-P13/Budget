using Budget.Aggregates;
using Budget.DTOs.ViewModel;

namespace Budget.Factories.BudgetViewModelFactory.Interfaces;

public interface IBudgetViewModelFactory
{
	public BudgetViewModel FromAggregate(Budget budget);
}