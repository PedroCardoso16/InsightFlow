using InsightFlow.Domain.Enums;

namespace InsightFlow.Domain.Entities;

public class Demand
{
    public Guid Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public DemandPriority Priority { get; set; }

    public DemandStatus Status { get; set; } = DemandStatus.Open;

    public Guid CategoryId { get; set; }

    public Category? Category { get; set; }

    public Guid CreatedByUserId { get; set; }

    public User? CreatedByUser { get; set; }

    public Guid? AssignedToUserId { get; set; }

    public User? AssignedToUser { get; set; }

    public string? Notes { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }

    public DateTime? CompletedAt { get; set; }
}