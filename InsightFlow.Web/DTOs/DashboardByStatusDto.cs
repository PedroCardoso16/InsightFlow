namespace InsightFlow.Web.DTOs;

public class DashboardByStatusDto
{
    public int Status { get; set; }

    public string StatusName { get; set; } = string.Empty;

    public int Total { get; set; }
}