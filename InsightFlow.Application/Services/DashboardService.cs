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

    public async Task<List<DashboardMonthlyDto>> GetMonthlyEvolutionAsync()
    {
        var demands = await _demandRepository.GetAllAsync();

        return demands
            .GroupBy(demand => new
            {
                demand.CreatedAt.Year,
                demand.CreatedAt.Month
            })
            .Select(group => new DashboardMonthlyDto
            {
                Year = group.Key.Year,
                Month = group.Key.Month,
                MonthName = GetMonthName(group.Key.Month),
                Total = group.Count()
            })
            .OrderBy(item => item.Year)
            .ThenBy(item => item.Month)
            .ToList();
    }

    public async Task<List<DashboardPriorityPercentageDto>> GetPriorityPercentagesAsync()
    {
        var demands = await _demandRepository.GetAllAsync();

        var total = demands.Count;

        if (total == 0)
            return new List<DashboardPriorityPercentageDto>();

        return demands
            .GroupBy(demand => demand.Priority)
            .Select(group => new DashboardPriorityPercentageDto
            {
                Priority = group.Key,
                PriorityName = group.Key.ToString(),
                Total = group.Count(),
                Percentage = Math.Round((decimal)group.Count() / total * 100, 2)
            })
            .OrderBy(item => item.Priority)
            .ToList();
    }

    public async Task<List<DashboardTopCategoryDto>> GetTopCategoriesAsync()
    {
        var demands = await _demandRepository.GetAllAsync();

        return demands
            .Where(demand => demand.Category is not null)
            .GroupBy(demand => new
            {
                demand.CategoryId,
                demand.Category!.Name
            })
            .Select(group => new DashboardTopCategoryDto
            {
                CategoryId = group.Key.CategoryId,
                CategoryName = group.Key.Name,
                Total = group.Count()
            })
            .OrderByDescending(item => item.Total)
            .Take(5)
            .ToList();
    }

    public async Task<DashboardResolutionTimeDto> GetAverageResolutionTimeAsync()
    {
        var demands = await _demandRepository.GetAllAsync();

        var completedDemands = demands
            .Where(demand =>
                demand.Status == DemandStatus.Completed &&
                demand.CompletedAt.HasValue)
            .ToList();

        if (!completedDemands.Any())
        {
            return new DashboardResolutionTimeDto
            {
                CompletedDemands = 0,
                AverageResolutionTimeInHours = 0,
                AverageResolutionTimeInDays = 0
            };
        }

        var averageHours = completedDemands
            .Average(demand => (demand.CompletedAt!.Value - demand.CreatedAt).TotalHours);

        return new DashboardResolutionTimeDto
        {
            CompletedDemands = completedDemands.Count,
            AverageResolutionTimeInHours = Math.Round(averageHours, 2),
            AverageResolutionTimeInDays = Math.Round(averageHours / 24, 2)
        };
    }

    private static string GetMonthName(int month)
    {
        return month switch
        {
            1 => "Janeiro",
            2 => "Fevereiro",
            3 => "Março",
            4 => "Abril",
            5 => "Maio",
            6 => "Junho",
            7 => "Julho",
            8 => "Agosto",
            9 => "Setembro",
            10 => "Outubro",
            11 => "Novembro",
            12 => "Dezembro",
            _ => "Mês inválido"
        };
    }
}