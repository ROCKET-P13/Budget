using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.DTOs.Requests;
using Server.Factories.BudgetFactory.Interfaces;
using Server.Factories.BudgetViewModelFactory.Interfaces;
using Server.Finders.BudgetFinder.Interfaces;
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
	ICategoryRepository categoryRepository,
	IBudgetFinder budgetFinder
) : ControllerBase
{
	private readonly IBudgetRepository _budgetRepository = budgetRepository;
	private readonly IBudgetFactory _budgetFactory = budgetFactory;
	private readonly IBudgetViewModelFactory _budgetViewModelFactory = budgetViewModelFactory;
	private readonly ICategoryRepository _categoryRepository = categoryRepository;
	private readonly IBudgetFinder _budgetFinder = budgetFinder;
	
	[HttpGet]
	[Authorize]
	public async Task<IActionResult> GetAll()
	{
		var budgets = await _budgetFinder.GetAll();
		return Ok(budgets);
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

	[HttpPost]
	public async Task<IActionResult> Create([FromBody] CreateBudgetRequest request)
	{
		var budget = _budgetFactory.Create(request.Name);

		await _budgetRepository.SaveAsync(budget);
		return Ok(new
		{
			budget.Id,
			budget.Name,
			budget.CreatedAt
		});
	}

	[HttpGet("{budgetId}/categories")]
	public async Task<IActionResult> GetBudgetCategories([FromRoute] Guid budgetId)
	{
		var budget = await _budgetRepository.GetById(budgetId);
		return Ok(budget.Categories);
	}

	[HttpPost("{budgetId}/categories")]
	public async Task<IActionResult> AddCategory([FromBody] AddCategoryToBudgetRequest request, [FromRoute] Guid budgetId)
	{
		var category = await _categoryRepository.GetById(request.CategoryId) ?? throw new Exception("Invalid Category");
        var budget = await _budgetRepository.GetById(budgetId) ?? throw new Exception("Invalid Budget");

		budget.AddCategory(request.PlannedAmount, category);

		await _budgetRepository.SaveAsync(budget);
		return Ok(_budgetViewModelFactory.FromAggregate(budget));
	}

	[HttpPost("{budgetId}/transactions")]
	public async Task<IActionResult> AddTransaction([FromBody] AddTransactionToBudgetRequest request,[FromRoute] Guid budgetId)
	{
		var budget = await _budgetRepository.GetById(budgetId) ?? throw new Exception("Budget not found.");
		budget.AddTransaction(
			request.CategoryId,
			request.Merchant,
			request.Amount,
			request.Date,
			request.Description
		);
		
		await _budgetRepository.SaveAsync(budget);
		return Ok(_budgetViewModelFactory.FromAggregate(budget));
	}

}