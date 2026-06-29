using InsightFlow.Domain.Enums;

namespace InsightFlow.Application.DTOs;

public class DashboardPriorityPercentageDto
{
    public DemandPriority Priority { get; set; }

    public string PriorityName { get; set; } = string.Empty;

    public int Total { get; set; }

    public decimal Percentage { get; set; }
}