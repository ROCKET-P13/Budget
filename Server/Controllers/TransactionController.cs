using Microsoft.AspNetCore.Mvc;
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

	[HttpPut]
	public async Task<IActionResult> Update([FromBody] UpdateTransactionRequest request)
	 {
		var budget = await _budgetRepository.GetById(request.BudgetId);
		return Ok(_budgetViewModelFactory.FromAggregate(budget));
	 }
}