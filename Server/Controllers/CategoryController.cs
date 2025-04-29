using Microsoft.AspNetCore.Mvc;
using Server.DTOs;
using Server.Repositories.BudgetRepository.Interfaces;

namespace Server.Controllers;

[ApiController]
[Route("api/[controller]")]

public class CategoryController(IBudgetRepository repo) : ControllerBase
{
	private readonly IBudgetRepository _budgetRepository = repo;

	[HttpPost]
	public async Task<IActionResult> Create([FromBody] CreateCategoryRequest request)
	{
		var budget = await _budgetRepository.GetById(request.BudgetId);
		budget.AddCategory(request.Name, request.SpendingLimit);

		await _budgetRepository.SaveAsync(budget);

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

	[HttpPut]
	public async Task<IActionResult> Update ([FromBody] UpdateCategoryRequest request)
	{
		var budget = await _budgetRepository.GetById(request.BudgetId);
		budget.UpdateCategory(request.CategoryId, request.SpendingLimit, request.Name);

		await _budgetRepository.SaveAsync(budget);

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