using InsightFlow.Application.DTOs;
using InsightFlow.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InsightFlow.Api.Controllers;

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
    public async Task<IActionResult> Login(LoginDto loginDto)
    {
        if (string.IsNullOrWhiteSpace(loginDto.Email))
            return BadRequest("O e-mail é obrigatório.");

        if (string.IsNullOrWhiteSpace(loginDto.Password))
            return BadRequest("A senha é obrigatória.");

        var response = await _authService.LoginAsync(loginDto);

        if (response is null)
            return Unauthorized("E-mail ou senha inválidos.");

        return Ok(response);
    }
}