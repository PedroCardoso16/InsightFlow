using InsightFlow.Application.DTOs;
using InsightFlow.Application.Interfaces;
using InsightFlow.Domain.Enums;

namespace InsightFlow.Application.Services;

public class DashboardService : IDashboardService
{
    private readonly IDemandRepository _demandRepository;

    public DashboardService(IDemandRepository demandRepository)
    {
        _demandRepository = demandRepository;
    }

    public async Task<DashboardSummaryDto> GetSummaryAsync()
    {
        var demands = await _demandRepository.GetAllAsync();

        var total = demands.Count;
        var completed = demands.Count(demand => demand.Status == DemandStatus.Completed);

        var completionRate = total == 0
            ? 0
            : Math.Round((decimal)completed / total * 100, 2);

        return new DashboardSummaryDto
        {
            TotalDemands = total,
            OpenDemands = demands.Count(demand => demand.Status == DemandStatus.Open),
            InProgressDemands = demands.Count(demand => demand.Status == DemandStatus.InProgress),
            CompletedDemands = completed,
            CanceledDemands = demands.Count(demand => demand.Status == DemandStatus.Canceled),
            CompletionRate = completionRate
        };
    }

    public async Task<List<DashboardByStatusDto>> GetByStatusAsync()
    {
        var demands = await _demandRepository.GetAllAsync();

        return demands
            .GroupBy(demand => demand.Status)
            .Select(group => new DashboardByStatusDto
            {
                Status = group.Key,
                StatusName = group.Key.ToString(),
                Total = group.Count()
            })
            .OrderBy(item => item.Status)
            .ToList();
    }

    public async Task<List<DashboardByPriorityDto>> GetByPriorityAsync()
    {
        var demands = await _demandRepository.GetAllAsync();

        return demands
            .GroupBy(demand => demand.Priority)
            .Select(group => new DashboardByPriorityDto
            {
                Priority = group.Key,
                PriorityName = group.Key.ToString(),
                Total = group.Count()
            })
            .OrderBy(item => item.Priority)
            .ToList();
    }

    public async Task<List<DashboardByCategoryDto>> GetByCategoryAsync()
    {
        var demands = await _demandRepository.GetAllAsync();

        return demands
            .Where(demand => demand.Category is not null)
            .GroupBy(demand => new
            {
                demand.CategoryId,
                demand.Category!.Name
            })
            .Select(group => new DashboardByCategoryDto
            {
                CategoryId = group.Key.CategoryId,
                CategoryName = group.Key.Name,
                Total = group.Count()
            })
            .OrderByDescending(item => item.Total)
            .ToList();
    }
}