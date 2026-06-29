namespace InsightFlow.Web.DTOs;

public class DashboardSummaryDto
{
    public int TotalDemands { get; set; }

    public int OpenDemands { get; set; }

    public int InProgressDemands { get; set; }

    public int CompletedDemands { get; set; }

    public int CanceledDemands { get; set; }

    public decimal CompletionRate { get; set; }
}