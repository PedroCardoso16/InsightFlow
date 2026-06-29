namespace InsightFlow.Web.DTOs;

public class DashboardTopCategoryDto
{
    public Guid CategoryId { get; set; }

    public string CategoryName { get; set; } = string.Empty;

    public int Total { get; set; }
}