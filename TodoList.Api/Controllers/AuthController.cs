using Microsoft.AspNetCore.Mvc;
using TodoList.Api.DTOs.Auth;
using TodoList.Api.Services;

namespace TodoList.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<ActionResult<AuthResponse>> Login(LoginRequest request)
    {
        var user = await _authService.ValidateUserAsync(request.Username, request.Password);
        if (user == null)
            return Unauthorized(new { message = "Invalid username or password" });

        var token = _authService.GenerateJwtToken(user);
        return new AuthResponse(token, user.Username);
    }

    [HttpPost("register")]
    public async Task<ActionResult<AuthResponse>> Register(RegisterRequest request)
    {
        var user = await _authService.RegisterUserAsync(request.Username, request.Password);
        if (user == null)
            return BadRequest(new { message = "Username already exists" });

        var token = _authService.GenerateJwtToken(user);
        return new AuthResponse(token, user.Username);
    }
}
