using BookStore.Infrastructure.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TestController : ControllerBase
{
    [HttpGet]
    public IActionResult Test()
    {
        var password = new PasswordHasher<ApplicationUser>();
        return Ok(password.HashPassword(null!, "User@123"));
    }
}
