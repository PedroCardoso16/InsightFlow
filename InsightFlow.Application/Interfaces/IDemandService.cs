using InsightFlow.Application.DTOs;

namespace InsightFlow.Application.Interfaces;

public interface IDemandService
{
    Task<List<DemandDto>> GetAllAsync(DemandFilterDto? filter = null);

    Task<DemandDto?> GetByIdAsync(Guid id);

    Task<DemandDto> CreateAsync(CreateDemandDto createDemandDto);

    Task<DemandDto?> UpdateAsync(Guid id, UpdateDemandDto updateDemandDto);

    Task<DemandDto?> UpdateStatusAsync(Guid id, UpdateDemandStatusDto updateDemandStatusDto);

    Task<bool> DeleteAsync(Guid id);
}