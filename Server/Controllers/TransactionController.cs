using Microsoft.AspNetCore.Mvc;
using Server.DTOs;
using Server.DTOs.Requests;
using Server.Repositories.BudgetRepository.Interfaces;

namespace Server.Controllers;

[ApiController]
[Route("api/[controller]")]

public class TransactionController(IBudgetRepository repo) : ControllerBase
{
	private readonly IBudgetRepository _budgetRepository = repo;

	[HttpPost]
	public async Task<IActionResult> Create([FromBody] CreateTransactionRequest request)
	{
		var budget = await _budgetRepository.GetById(request.BudgetId);

		budget.AddTransaction(request.CategoryId, request.Merchant, request.Amount, request.Date, request.Description);

		await _budgetRepository.SaveAsync(budget);

		
		var budgetDTO = new BudgetDTO
		{
			Id = budget.Id,
			Name = budget.Name,
			Categories = [
				.. budget.Categories.Select(c => new CategoryDTO
				{
					Id = c.Id,
					Name = c.Name,
					SpendingLimit = c.SpendingLimit,
					Transactions = [
						.. budget.Transactions
						.Where(t => t.CategoryId == c.Id)
						.Select(t => new TransactionDTO
						{
							Id = t.Id,
							CategoryId = t.CategoryId,
							Description = t.Description,
							Merchant = t.Merchant,
							Amount = t.Amount,
							Date = t.Date
						}).ToList()
					]
				}).ToList()
			]
		};

		return Ok(budgetDTO);
	}
}