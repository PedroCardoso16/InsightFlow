using InsightFlow.Domain.Enums;

namespace InsightFlow.Application.DTOs;

public class UpdateUserDto
{
    public string Name { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public UserRole Role { get; set; }

    public bool IsActive { get; set; }
}