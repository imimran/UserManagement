using Microsoft.AspNetCore.Mvc;
using UserManagement.Api.Application.DTOs.Auth;
using UserManagement.Api.Application.Interfaces;
using UserManagement.Api.Common.Responses;

namespace UserManagement.Api.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }


    [HttpPost("register")]
    public async Task<IActionResult> Register(
        RegisterRequest request)
    {
        var result =
            await _authService.RegisterAsync(request);

        return StatusCode(
            StatusCodes.Status201Created,
            ApiResponse<LoginResponse>.Ok(
                result,
                "Registration successful."));
    }


    [HttpPost("login")]
    public async Task<IActionResult> Login(
        LoginRequest request)
    {
        var result =
            await _authService.LoginAsync(request);

        return Ok(
            ApiResponse<LoginResponse>.Ok(
                result,
                "Login successful."));
    }
}