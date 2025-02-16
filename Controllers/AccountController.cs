using BlogApi.Models;
using BlogApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace BlogApi.Controllers;

[ApiController]
public class AccountController : ControllerBase
{
    [HttpPost("v1/login")]
    public IActionResult Login([FromServices] TokenService tokenService)
    {
        var token = tokenService.GenerateToken(new User());

        return Ok(token);
    }
}

