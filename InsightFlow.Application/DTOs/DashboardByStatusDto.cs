using InsightFlow.Domain.Enums;

namespace InsightFlow.Application.DTOs;

public class DashboardByStatusDto
{
    public DemandStatus Status { get; set; }

    public string StatusName { get; set; } = string.Empty;

    public int Total { get; set; }
}