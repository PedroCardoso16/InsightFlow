namespace InsightFlow.Web.DTOs;

public class DashboardResolutionTimeDto
{
    public int CompletedDemands { get; set; }

    public double AverageResolutionTimeInHours { get; set; }

    public double AverageResolutionTimeInDays { get; set; }
}