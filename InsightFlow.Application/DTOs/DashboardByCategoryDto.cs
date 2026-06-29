namespace InsightFlow.Application.DTOs;

public class DashboardByCategoryDto
{
    public Guid CategoryId { get; set; }

    public string CategoryName { get; set; } = string.Empty;

    public int Total { get; set; }
}