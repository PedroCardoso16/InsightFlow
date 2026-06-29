using InsightFlow.Application.DTOs;

namespace InsightFlow.Application.Interfaces;

public interface IAuthService
{
    Task<AuthResponseDto?> LoginAsync(LoginDto loginDto);
}