namespace InsightFlow.Web.DTOs;

public class DashboardByPriorityDto
{
    public int Priority { get; set; }

    public string PriorityName { get; set; } = string.Empty;

    public int Total { get; set; }
}