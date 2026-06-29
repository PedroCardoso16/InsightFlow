using InsightFlow.Application.DTOs;

namespace InsightFlow.Application.Interfaces;

public interface IDashboardService
{
    Task<DashboardSummaryDto> GetSummaryAsync();

    Task<List<DashboardByStatusDto>> GetByStatusAsync();

    Task<List<DashboardByPriorityDto>> GetByPriorityAsync();

    Task<List<DashboardByCategoryDto>> GetByCategoryAsync();

    Task<List<DashboardMonthlyDto>> GetMonthlyEvolutionAsync();

    Task<List<DashboardPriorityPercentageDto>> GetPriorityPercentagesAsync();

    Task<List<DashboardTopCategoryDto>> GetTopCategoriesAsync();

    Task<DashboardResolutionTimeDto> GetAverageResolutionTimeAsync();
}