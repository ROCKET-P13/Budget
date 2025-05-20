using Server.DTOs.Projection;

namespace Server.Finders.BudgetFinder.Interfaces;

public interface IBudgetFinder
{
	Task<List<BudgetProjection>> GetAll();
}