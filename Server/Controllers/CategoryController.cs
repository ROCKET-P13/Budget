
using Microsoft.AspNetCore.Mvc;
using Server.Models;
using Server.Repositories.CategoryRepository.Interfaces;

[ApiController]
[Route("api/[controller]")]
public class CategoryController(ICategoryRepository repo): ControllerBase
{
	private readonly ICategoryRepository _categoryRepository = repo;
	[HttpPost]
	public async Task<IActionResult> Create(Category category)
	{
		var created = await _categoryRepository.Create(category);
		return Ok(created);
	}
}