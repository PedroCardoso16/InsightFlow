using InsightFlow.Application.DTOs;
using InsightFlow.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace InsightFlow.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var users = await _userService.GetAllAsync();

        return Ok(users);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var user = await _userService.GetByIdAsync(id);

        if (user is null)
            return NotFound("Usuário não encontrado.");

        return Ok(user);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateUserDto createUserDto)
    {
        if (string.IsNullOrWhiteSpace(createUserDto.Name))
            return BadRequest("O nome do usuário é obrigatório.");

        if (string.IsNullOrWhiteSpace(createUserDto.Email))
            return BadRequest("O e-mail do usuário é obrigatório.");

        if (string.IsNullOrWhiteSpace(createUserDto.Password))
            return BadRequest("A senha do usuário é obrigatória.");

        try
        {
            var user = await _userService.CreateAsync(createUserDto);

            return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, UpdateUserDto updateUserDto)
    {
        if (string.IsNullOrWhiteSpace(updateUserDto.Name))
            return BadRequest("O nome do usuário é obrigatório.");

        if (string.IsNullOrWhiteSpace(updateUserDto.Email))
            return BadRequest("O e-mail do usuário é obrigatório.");

        try
        {
            var user = await _userService.UpdateAsync(id, updateUserDto);

            if (user is null)
                return NotFound("Usuário não encontrado.");

            return Ok(user);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deleted = await _userService.DeleteAsync(id);

        if (!deleted)
            return NotFound("Usuário não encontrado.");

        return NoContent();
    }
}