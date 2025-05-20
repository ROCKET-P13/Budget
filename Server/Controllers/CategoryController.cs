using Microsoft.AspNetCore.Mvc;
using Server.DTOs.Requests;
using Server.Factories.CategoryFactory.Interfaces;
using Server.Finders.CategoryFinder.Interfaces;
using Server.Repositories.CategoryRepository.Interfaces;

namespace Server.Controllers;

[ApiController]
[Route("api/[controller]")]

public class CategoryController(
	ICategoryRepository categoryRepository,
	ICategoryFactory categoryFactory,
	ICategoryFinder categoryFinder
	) : ControllerBase
{
	private readonly ICategoryRepository _categoryRepository = categoryRepository;
	private readonly ICategoryFactory _categoryFactory = categoryFactory;
	private readonly ICategoryFinder _categoryFinder = categoryFinder;

	[HttpGet]
	public async Task<IActionResult> Get()
	{
		var categories = await _categoryFinder.GetAll();
		return Ok(categories);
	}

	[HttpPost]
	public async Task<IActionResult> Create([FromBody] CreateCategoryRequest request)
	{
		var category = _categoryFactory.Create(request.Name, request.IsDebt);

		await _categoryRepository.SaveAsync(category);

		return Ok(new
		{
			category.Id,
			category.Name,
			category.IsDebt,
			category.CreatedAt
		});
	}

	[HttpPut("{categoryId}")]
	public async Task<IActionResult> Update([FromRoute] Guid categoryId, [FromBody] UpdateCategoryRequest request)
	{
		var category = await _categoryRepository.GetById(categoryId) ?? throw new Exception("Category not found");
		category.UpdateName(request.Name);

		await _categoryRepository.SaveAsync(category);
		return Ok(new
		{
			category.Id,
			category.Name,
			category.IsDebt,
			category.CreatedAt
		});
	}
}