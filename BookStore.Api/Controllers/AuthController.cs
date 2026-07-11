using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Api.Controllers;

[Route("[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    [HttpGet("login")]
    public async Task<IActionResult> Login()
    {
        return Ok();
    }
}
