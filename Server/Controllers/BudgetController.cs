using Microsoft.AspNetCore.Mvc;
using Server.DTOs;
using Server.Factories.BudgetFactory.Interfaces;
using Server.Repositories.BudgetRepository.Interfaces;

namespace Server.Controllers;
[ApiController]
[Route("api/[controller]")]

public class BudgetController(IBudgetRepository repo, IBudgetFactory factory) : ControllerBase
{
	private readonly IBudgetRepository _budgetRepository = repo;
	private readonly IBudgetFactory _budgetFactory = factory;

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

		var budgetDTO = new BudgetDTO
		{
			Id = budget.Id,
			Name = budget.Name,
			Categories = [.. budget.Categories.Select(c => new CategoryDTO
			{
				Id = c.Id,
				Name = c.Name,
				SpendingLimit = c.SpendingLimit,
				Transactions = [.. c.Transactions.Select(t => new TransactionDTO
				{
					Id = t.Id,
					Merchant = t.Merchant,
					Amount = t.Amount,
					Date = t.Date,
				})]
			})]
		};

		return Ok(budgetDTO);
	}
}