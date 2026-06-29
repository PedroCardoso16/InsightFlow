namespace InsightFlow.Web.DTOs;

public class DashboardPriorityPercentageDto
{
    public int Priority { get; set; }

    public string PriorityName { get; set; } = string.Empty;

    public int Total { get; set; }

    public decimal Percentage { get; set; }
}