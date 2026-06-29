using InsightFlow.Domain.Enums;

namespace InsightFlow.Application.DTOs;

public class DemandFilterDto
{
    public DemandStatus? Status { get; set; }

    public DemandPriority? Priority { get; set; }

    public Guid? CategoryId { get; set; }

    public Guid? CreatedByUserId { get; set; }

    public Guid? AssignedToUserId { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public string? Search { get; set; }
}