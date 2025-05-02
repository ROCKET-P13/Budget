using Microsoft.AspNetCore.Mvc;
using Server.DTOs;
using Server.DTOs.Requests;
using Server.Factories.BudgetViewModelFactory.Interfaces;
using Server.Repositories.BudgetRepository.Interfaces;

namespace Server.Controllers;

[ApiController]
[Route("api/[controller]")]

public class TransactionController(IBudgetRepository budgetRepository, IBudgetViewModelFactory budgetViewModelFactory) : ControllerBase
{
	private readonly IBudgetRepository _budgetRepository = budgetRepository;
	private readonly IBudgetViewModelFactory _budgetViewModelFactory = budgetViewModelFactory;

	[HttpPost]
	public async Task<IActionResult> Create([FromBody] CreateTransactionRequest request)
	{
		var budget = await _budgetRepository.GetById(request.BudgetId);

		budget.AddTransaction(request.CategoryId, request.Merchant, request.Amount, request.Date, request.Description);

		await _budgetRepository.SaveAsync(budget);

		return Ok(_budgetViewModelFactory.FromAggregate(budget));
	}

	[HttpPut]
	public async Task<IActionResult> Update([FromBody] UpdateTransactionRequest request)
	 {
		var budget = await _budgetRepository.GetById(request.BudgetId);


		return Ok(_budgetViewModelFactory.FromAggregate(budget));
	 }
}