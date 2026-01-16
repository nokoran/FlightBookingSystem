using FlightBookingSystem.Application.DTOs;
using FlightBookingSystem.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FlightBookingSystem.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto dto)
    {
        var result = await _authService.RegisterAsync(dto);
        if (!result)
            return BadRequest("Не вдалося створити користувача (можливо email зайнятий або пароль занадто простий).");

        return Ok(new { message = "Реєстрація успішна!" });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        var token = await _authService.LoginAsync(dto);
        if (token == null)
            return Unauthorized("Невірний логін або пароль.");

        return Ok(new { token = token });
    }
}