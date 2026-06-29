using InsightFlow.Domain.Enums;

namespace InsightFlow.Application.DTOs;

public class UpdateDemandDto
{
    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public DemandPriority Priority { get; set; }

    public DemandStatus Status { get; set; }

    public Guid CategoryId { get; set; }

    public Guid? AssignedToUserId { get; set; }

    public string? Notes { get; set; }
}