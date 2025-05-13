using Server.Aggregates;
using Server.Events.Category;
using Server.Factories.CategoryFactory.Interfaces;

namespace Server.Factories.CategoryFactory;

public class CategoryFactory : ICategoryFactory
{
	public Category Create(string name)
	{
		var createdCategoryEvent = new CreatedCategory
		{
			CategoryName = name,
			CategoryId = Guid.NewGuid()
		};

		var category = new Category([createdCategoryEvent]);

		return category;
	}
}