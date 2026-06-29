using InsightFlow.Domain.Entities;

namespace InsightFlow.Application.Interfaces;

public interface IDemandRepository
{
    Task<List<Demand>> GetAllAsync();

    Task<Demand?> GetByIdAsync(Guid id);

    Task<Demand> CreateAsync(Demand demand);

    Task<Demand?> UpdateAsync(Demand demand);

    Task<bool> DeleteAsync(Guid id);
}