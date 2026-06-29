using InsightFlow.Application.DTOs;
using InsightFlow.Domain.Entities;

namespace InsightFlow.Application.Interfaces;

public interface IDemandRepository
{
    Task<List<Demand>> GetAllAsync(DemandFilterDto? filter = null);

    Task<Demand?> GetByIdAsync(Guid id);

    Task<Demand> CreateAsync(Demand demand);

    Task<Demand?> UpdateAsync(Demand demand);

    Task<bool> DeleteAsync(Guid id);
}