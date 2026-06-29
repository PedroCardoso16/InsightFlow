using System.Net.Http.Json;
using InsightFlow.Web.DTOs;

namespace InsightFlow.Web.Services;

public class DashboardService
{
    private readonly HttpClient _httpClient;

    public DashboardService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<DashboardSummaryDto?> GetSummaryAsync()
    {
        return await _httpClient.GetFromJsonAsync<DashboardSummaryDto>("api/Dashboard/summary");
    }

    public async Task<List<DashboardByStatusDto>> GetByStatusAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<DashboardByStatusDto>>("api/Dashboard/by-status")
            ?? new List<DashboardByStatusDto>();
    }

    public async Task<List<DashboardByPriorityDto>> GetByPriorityAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<DashboardByPriorityDto>>("api/Dashboard/by-priority")
            ?? new List<DashboardByPriorityDto>();
    }

    public async Task<List<DashboardByCategoryDto>> GetByCategoryAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<DashboardByCategoryDto>>("api/Dashboard/by-category")
            ?? new List<DashboardByCategoryDto>();
    }

    public async Task<List<DashboardMonthlyDto>> GetMonthlyEvolutionAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<DashboardMonthlyDto>>("api/Dashboard/monthly-evolution")
            ?? new List<DashboardMonthlyDto>();
    }

    public async Task<List<DashboardPriorityPercentageDto>> GetPriorityPercentagesAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<DashboardPriorityPercentageDto>>("api/Dashboard/priority-percentages")
            ?? new List<DashboardPriorityPercentageDto>();
    }

    public async Task<List<DashboardTopCategoryDto>> GetTopCategoriesAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<DashboardTopCategoryDto>>("api/Dashboard/top-categories")
            ?? new List<DashboardTopCategoryDto>();
    }

    public async Task<DashboardResolutionTimeDto?> GetAverageResolutionTimeAsync()
    {
        return await _httpClient.GetFromJsonAsync<DashboardResolutionTimeDto>("api/Dashboard/average-resolution-time");
    }
}