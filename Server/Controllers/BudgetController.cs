using Microsoft.AspNetCore.Mvc;
using Server.DTOs.Requests;
using Server.Factories.BudgetFactory.Interfaces;
using Server.Factories.BudgetViewModelFactory.Interfaces;
using Server.Repositories.BudgetRepository.Interfaces;
using Server.Repositories.CategoryRepository.Interfaces;

namespace Server.Controllers;
[ApiController]
[Route("api/[controller]")]

public class BudgetController
(
	IBudgetRepository budgetRepository,
	IBudgetFactory budgetFactory,
	IBudgetViewModelFactory budgetViewModelFactory,
	ICategoryRepository categoryRepository
) : ControllerBase
{
	private readonly IBudgetRepository _budgetRepository = budgetRepository;
	private readonly IBudgetFactory _budgetFactory = budgetFactory;
	private readonly IBudgetViewModelFactory _budgetViewModelFactory = budgetViewModelFactory;
	private readonly ICategoryRepository _categoryRepository = categoryRepository;

	[HttpPost]
	public async Task<IActionResult> Create([FromBody] CreateBudgetRequest request)
	{
		var budget = _budgetFactory.Create(request.Name);

		await _budgetRepository.SaveAsync(budget);
		return Ok(new
		{
			budget.Id,
			budget.Name,
		});
	}

	[HttpPost("{budgetId}/categories")]
	public async Task<IActionResult> AddCategory([FromBody] AddCategoryToBudgetRequest request, [FromRoute] Guid budgetId)
	{
		var category = await _categoryRepository.GetById(request.CategoryId) ?? throw new Exception("Invalid Category");
        var budget = await _budgetRepository.GetById(budgetId) ?? throw new Exception("Invalid Budget");

		budget.AddCategory(category.Name, request.PlannedAmount, category.Id);

		await _budgetRepository.SaveAsync(budget);
		return Ok(_budgetViewModelFactory.FromAggregate(budget));

	}

	[HttpGet("{id}")]
	public async Task<IActionResult> Get(Guid id)
	{
		var budget = await _budgetRepository.GetById(id);
		if (budget == null)
		{
			return NotFound();
		}


		return Ok(_budgetViewModelFactory.FromAggregate(budget));
	}
}