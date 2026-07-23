using BookStore.Application.Common.Contracts;
using BookStore.Application.Features.Categories.Contracts.Requests;
using BookStore.Application.Features.Categories.Services;

namespace BookStore.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class CategoriesController(ICategoryService categoryService) : ControllerBase
{
    private readonly ICategoryService _categoryService = categoryService;

    [HttpGet("")]
    public async Task<IActionResult> GetAll([FromQuery] RequestFilters filters, CancellationToken cancellationToken)
    {
        return Ok(await _categoryService.GetAllAsync(filters, cancellationToken));
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> Get([FromRoute] int id, CancellationToken cancellationToken)
    {
        var result = await _categoryService.GetAsync(id, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpPost("")]
    public async Task<IActionResult> Add([FromBody] CategoryRequest request, CancellationToken cancellationToken)
    {
        var result = await _categoryService.AddAsync(request, cancellationToken);
        return result.IsSuccess ? CreatedAtAction(nameof(Get), new { id = result.Value }, result.Value) : result.ToProblem();
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] CategoryRequest request, CancellationToken cancellationToken)
    {
        var result = await _categoryService.UpdateAsync(id, request, cancellationToken);
        return result.IsSuccess ? NoContent() : result.ToProblem();
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id, CancellationToken cancellationToken)
    {
        var result = await _categoryService.DeleteAsync(id, cancellationToken);
        return result.IsSuccess ? NoContent() : result.ToProblem();
    }
    [HttpPut("restore/{id}")]
    public async Task<IActionResult> Restore([FromRoute] int id, CancellationToken cancellationToken)
    {
        var result = await _categoryService.RestoreAsync(id, cancellationToken);
        return result.IsSuccess ? NoContent() : result.ToProblem();
    }
}
