namespace InsightFlow.Application.DTOs;

public class DashboardMonthlyDto
{
    public int Year { get; set; }

    public int Month { get; set; }

    public string MonthName { get; set; } = string.Empty;

    public int Total { get; set; }
}