using InsightFlow.Domain.Enums;

namespace InsightFlow.Application.DTOs;

public class DemandDto
{
    public Guid Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public DemandPriority Priority { get; set; }

    public DemandStatus Status { get; set; }

    public Guid CategoryId { get; set; }

    public string? CategoryName { get; set; }

    public Guid CreatedByUserId { get; set; }

    public string? CreatedByUserName { get; set; }

    public Guid? AssignedToUserId { get; set; }

    public string? AssignedToUserName { get; set; }

    public string? Notes { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? CompletedAt { get; set; }
}