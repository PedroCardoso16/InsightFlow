using InsightFlow.Domain.Enums;

namespace InsightFlow.Application.DTOs;

public class CreateDemandDto
{
    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public DemandPriority Priority { get; set; }

    public Guid CategoryId { get; set; }

    public Guid CreatedByUserId { get; set; }

    public Guid? AssignedToUserId { get; set; }

    public string? Notes { get; set; }
}