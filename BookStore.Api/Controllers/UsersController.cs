using BookStore.Application.Features.User.Contracts.Requests;

namespace BookStore.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class UsersController(IUserService userService) : ControllerBase
{
    private readonly IUserService _userService = userService;

    [HttpGet("")]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        return Ok(await _userService.GetAllAsync(cancellationToken));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(string id)
    {
        var result = await _userService.GetAsync(id);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpPost("")]
    public async Task<IActionResult> Add(CreateUserRequest request, CancellationToken cancellationToken)
    {
        var result = await _userService.AddAsync(request, cancellationToken);
        return result.IsSuccess ? CreatedAtAction(nameof(Get), new { result.Value.Id }, result.Value) : result.ToProblem();
    }

    [HttpPost("confirm-email-and-set-password")]
    [AllowAnonymous]
    public async Task<IActionResult> ConfirmEmailWithSetPassword(ConfirmEmailAndSetPasswordRequest request, CancellationToken cancellationToken)
    {
        var result = await _userService.ConfirmEmailAndSetPasswordAsync(request);
        return result.IsSuccess ? Ok("Welcome, Email now confirmed and password has been set") : result.ToProblem();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, UpdateUserRequest request, CancellationToken cancellationToken)
    {
        var result = await _userService.UpdateAsync(id, request, cancellationToken);
        return result.IsSuccess ? NoContent() : result.ToProblem();
    }

    [HttpPut("{id}/toggle-status")]
    public async Task<IActionResult> ToggleStatus(string id, CancellationToken cancellationToken)
    {
        var result = await _userService.ToggleStatusAsync(id, cancellationToken);
        return result.IsSuccess ? NoContent() : result.ToProblem();
    }

    [HttpPut("{id}/unlock")]
    public async Task<IActionResult> Unlock(string id, CancellationToken cancellationToken)
    {
        var result = await _userService.UnlockAsync(id, cancellationToken);
        return result.IsSuccess ? NoContent() : result.ToProblem();
    }
}
