using Server.Aggregates;
using Server.DTOs.ViewModel;

namespace Server.Factories.BudgetViewModelFactory.Interfaces;

public interface IBudgetViewModelFactory
{
	public BudgetViewModel FromAggregate(Budget budget);
}