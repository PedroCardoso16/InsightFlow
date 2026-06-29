using InsightFlow.Domain.Enums;

namespace InsightFlow.Application.DTOs;

public class DashboardByPriorityDto
{
    public DemandPriority Priority { get; set; }

    public string PriorityName { get; set; } = string.Empty;

    public int Total { get; set; }
}