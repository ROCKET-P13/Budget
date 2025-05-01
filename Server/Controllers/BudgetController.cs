using Microsoft.AspNetCore.Mvc;
using Server.DTOs.Requests;
using Server.Factories.BudgetFactory.Interfaces;
using Server.Factories.BudgetViewModelFactory.Interfaces;
using Server.Repositories.BudgetRepository.Interfaces;

namespace Server.Controllers;
[ApiController]
[Route("api/[controller]")]

public class BudgetController(IBudgetRepository budgetRepository, IBudgetFactory budgetFactory, IBudgetViewModelFactory budgetViewModelFactory) : ControllerBase
{
	private readonly IBudgetRepository _budgetRepository = budgetRepository;
	private readonly IBudgetFactory _budgetFactory = budgetFactory;
	private readonly IBudgetViewModelFactory _budgetViewModelFactory = budgetViewModelFactory;

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

	[HttpGet("{id}")]
	public async Task<IActionResult> GetBudget(Guid id)
	{
		var budget = await _budgetRepository.GetById(id);
		if (budget == null)
		{
			return NotFound();
		}


		return Ok(_budgetViewModelFactory.FromAggregate(budget));
	}
}