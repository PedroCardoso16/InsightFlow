using InsightFlow.Application.DTOs;

namespace InsightFlow.Application.Interfaces;

public interface IUserService
{
    Task<List<UserDto>> GetAllAsync();

    Task<UserDto?> GetByIdAsync(Guid id);

    Task<UserDto> CreateAsync(CreateUserDto createUserDto);

    Task<UserDto?> UpdateAsync(Guid id, UpdateUserDto updateUserDto);

    Task<bool> DeleteAsync(Guid id);
}