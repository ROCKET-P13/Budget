using Microsoft.AspNetCore.Mvc;
using Server.DTOs;
using Server.DTOs.Requests;
using Server.Factories.BudgetViewModelFactory.Interfaces;
using Server.Repositories.BudgetRepository.Interfaces;

namespace Server.Controllers;

[ApiController]
[Route("api/[controller]")]

public class CategoryController(IBudgetRepository budgetRepository, IBudgetViewModelFactory budgetViewModelFactory) : ControllerBase
{
	private readonly IBudgetRepository _budgetRepository = budgetRepository;
	private readonly IBudgetViewModelFactory _budgetViewModelFactory = budgetViewModelFactory;

	[HttpPost]
	public async Task<IActionResult> Create([FromBody] CreateCategoryRequest request)
	{
		var budget = await _budgetRepository.GetById(request.BudgetId);
		budget.AddCategory(request.Name, request.PlannedAmount);

		await _budgetRepository.SaveAsync(budget);

		return Ok(_budgetViewModelFactory.FromAggregate(budget));
	}

	[HttpPatch]
	public async Task<IActionResult> Update ([FromBody] UpdateCategoryRequest request)
	{
		var budget = await _budgetRepository.GetById(request.BudgetId);
		budget.UpdateCategory(request.CategoryId, request.PlannedAmount, request.Name);

		await _budgetRepository.SaveAsync(budget);

		return Ok(_budgetViewModelFactory.FromAggregate(budget));

	}
}