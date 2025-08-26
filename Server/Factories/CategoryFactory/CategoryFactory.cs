using Budget.Aggregates;
using Budget.Events.Category;
using Budget.Factories.CategoryFactory.Interfaces;

namespace Budget.Factories.CategoryFactory;

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