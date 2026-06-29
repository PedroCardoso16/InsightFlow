using InsightFlow.Application.DTOs;
using InsightFlow.Application.Interfaces;
using InsightFlow.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace InsightFlow.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly PasswordHasher<User> _passwordHasher;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
        _passwordHasher = new PasswordHasher<User>();
    }

    public async Task<List<UserDto>> GetAllAsync()
    {
        var users = await _userRepository.GetAllAsync();

        return users.Select(user => new UserDto
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            Role = user.Role,
            IsActive = user.IsActive,
            CreatedAt = user.CreatedAt
        }).ToList();
    }

    public async Task<UserDto?> GetByIdAsync(Guid id)
    {
        var user = await _userRepository.GetByIdAsync(id);

        if (user is null)
            return null;

        return new UserDto
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            Role = user.Role,
            IsActive = user.IsActive,
            CreatedAt = user.CreatedAt
        };
    }

    public async Task<UserDto> CreateAsync(CreateUserDto createUserDto)
    {
        var existingUser = await _userRepository.GetByEmailAsync(createUserDto.Email);

        if (existingUser is not null)
            throw new InvalidOperationException("Já existe um usuário cadastrado com este e-mail.");

        var user = new User
        {
            Id = Guid.NewGuid(),
            Name = createUserDto.Name,
            Email = createUserDto.Email,
            Role = createUserDto.Role,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        user.PasswordHash = _passwordHasher.HashPassword(user, createUserDto.Password);

        var createdUser = await _userRepository.CreateAsync(user);

        return new UserDto
        {
            Id = createdUser.Id,
            Name = createdUser.Name,
            Email = createdUser.Email,
            Role = createdUser.Role,
            IsActive = createdUser.IsActive,
            CreatedAt = createdUser.CreatedAt
        };
    }

    public async Task<UserDto?> UpdateAsync(Guid id, UpdateUserDto updateUserDto)
    {
        var user = await _userRepository.GetByIdAsync(id);

        if (user is null)
            return null;

        var existingUser = await _userRepository.GetByEmailAsync(updateUserDto.Email);

        if (existingUser is not null && existingUser.Id != id)
            throw new InvalidOperationException("Já existe outro usuário cadastrado com este e-mail.");

        user.Name = updateUserDto.Name;
        user.Email = updateUserDto.Email;
        user.Role = updateUserDto.Role;
        user.IsActive = updateUserDto.IsActive;

        var updatedUser = await _userRepository.UpdateAsync(user);

        if (updatedUser is null)
            return null;

        return new UserDto
        {
            Id = updatedUser.Id,
            Name = updatedUser.Name,
            Email = updatedUser.Email,
            Role = updatedUser.Role,
            IsActive = updatedUser.IsActive,
            CreatedAt = updatedUser.CreatedAt
        };
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        return await _userRepository.DeleteAsync(id);
    }
}