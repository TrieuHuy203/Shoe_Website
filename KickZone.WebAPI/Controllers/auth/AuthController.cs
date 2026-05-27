using KickZone.Contracts.DTOs.Auth;
using KickZone.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace KickZone.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    // ================= REGISTER =================
    [HttpPost("register")]
    public async Task<IActionResult> Register(
        [FromBody] RegisterRequestDto request
    )
    {
        try
        {
            var result = await _authService.RegisterAsync(request);

            return Ok(new
            {
                success = true,
                message = result.Message,
                data = result
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new
            {
                success = false,
                message = ex.Message
            });
        }
    }

    // ================= LOGIN =================
    [HttpPost("login")]
    public async Task<IActionResult> Login(
        [FromBody] LoginRequestDto request
    )
    {
        try
        {
            var result = await _authService.LoginAsync(request);

            return Ok(new
            {
                success = true,
                message = "Login successfully",
                data = result
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new
            {
                success = false,
                message = ex.Message
            });
        }
    }
}