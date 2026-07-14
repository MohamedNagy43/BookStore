namespace BookStore.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class TestController : ControllerBase
{
    [HttpGet]
    public IActionResult Test()
    {
        var password = new PasswordHasher<ApplicationUser>();
        return Ok(password.HashPassword(null!, "User@123"));
    }
}
