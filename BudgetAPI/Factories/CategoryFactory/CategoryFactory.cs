using BudgetAPI.Aggregates;
using BudgetAPI.Events.Category;
using BudgetAPI.Factories.CategoryFactory.Interfaces;

namespace BudgetAPI.Factories.CategoryFactory;

public class CategoryFactory : ICategoryFactory
{
	public Category Create(string name, bool isDebt)
	{
		var createdCategoryEvent = new CreatedCategory
		{
			CategoryName = name,
			CategoryId = Guid.NewGuid(),
			CreatedAt = DateTime.UtcNow,
			IsDebt = isDebt
		};

		var category = new Category([createdCategoryEvent]);

		return category;
	}
}