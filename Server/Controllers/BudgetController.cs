using Microsoft.AspNetCore.Mvc;
using Server.DTOs;
using Server.Models;
using Server.Repositories.BudgetRepository.Interfaces;

[ApiController]
[Route("api/[controller]")]
public class BudgetController(IBudgetRepository repo): ControllerBase
{
	private readonly IBudgetRepository _budgetRepository = repo;

	[HttpPost]
	public async Task<IActionResult> Create(Budget budget)
	{
		var created = await _budgetRepository.Create(budget);
		return Ok(created);
	}

	[HttpGet("{id}")]
	public async Task<IActionResult> GetBudget(int id)
	{
		var budget = await _budgetRepository.GetBudgetById(id);
		if (budget == null)
		{
			return NotFound();
		}

		var budgetDTO = new BudgetDTO
		{
			Id = budget.Id,
			Name = budget.Name,
			CreatedAt = budget.CreatedDate,
			Categories = [.. budget.Categories.Select(c => new CategoryDTO
			{
				Id = c.Id,
				Name = c.Name,
				Amount = c.Amount,
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