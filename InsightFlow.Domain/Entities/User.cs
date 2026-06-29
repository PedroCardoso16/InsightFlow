using InsightFlow.Domain.Enums;

namespace InsightFlow.Domain.Entities;

public class User
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string PasswordHash { get; set; } = string.Empty;

    public UserRole Role { get; set; }

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<Demand> CreatedDemands { get; set; } = new List<Demand>();

    public ICollection<Demand> AssignedDemands { get; set; } = new List<Demand>();
}