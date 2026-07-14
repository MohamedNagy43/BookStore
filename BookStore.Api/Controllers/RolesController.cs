using BookStore.Application.Features.Role.Contracts.Requests;
using BookStore.Application.Features.Role.Services;

namespace BookStore.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RolesController(IRoleService roleService) : ControllerBase
{
    private readonly IRoleService _roleService = roleService;

    [HttpGet("")]
    public async Task<IActionResult> GetAll([FromQuery] bool includeDisabled, CancellationToken cancellationToken)
    {
        var roles = await _roleService.GetAllAsync(includeDisabled, cancellationToken);
        return Ok(roles);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(string id)
    {
        var result = await _roleService.GetAsync(id);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpPost("")]
    public async Task<IActionResult> Add(RoleRequest request, CancellationToken cancellationToken)
    {
        var result = await _roleService.AddAsync(request, cancellationToken);
        return result.IsSuccess ? CreatedAtAction(nameof(Get), new { result.Value.Id }, result.Value) : result.ToProblem();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, RoleRequest request, CancellationToken cancellationToken)
    {
        var result = await _roleService.UpdateAsync(id, request, cancellationToken);
        return result.IsSuccess ? NoContent() : result.ToProblem();
    }

    [HttpPut("{id}/toggle-status")]
    public async Task<IActionResult> ToggleStatus(string id, CancellationToken cancellationToken)
    {
        var result = await _roleService.ToggleStatusAsync(id, cancellationToken);
        return result.IsSuccess ? NoContent() : result.ToProblem();
    }
}
