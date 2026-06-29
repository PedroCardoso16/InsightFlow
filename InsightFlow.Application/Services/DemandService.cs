using InsightFlow.Application.DTOs;
using InsightFlow.Application.Interfaces;
using InsightFlow.Domain.Entities;
using InsightFlow.Domain.Enums;

namespace InsightFlow.Application.Services;

public class DemandService : IDemandService
{
    private readonly IDemandRepository _demandRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IUserRepository _userRepository;

    public DemandService(
        IDemandRepository demandRepository,
        ICategoryRepository categoryRepository,
        IUserRepository userRepository)
    {
        _demandRepository = demandRepository;
        _categoryRepository = categoryRepository;
        _userRepository = userRepository;
    }

    public async Task<List<DemandDto>> GetAllAsync()
    {
        var demands = await _demandRepository.GetAllAsync();

        return demands.Select(MapToDto).ToList();
    }

    public async Task<DemandDto?> GetByIdAsync(Guid id)
    {
        var demand = await _demandRepository.GetByIdAsync(id);

        if (demand is null)
            return null;

        return MapToDto(demand);
    }

    public async Task<DemandDto> CreateAsync(CreateDemandDto createDemandDto)
    {
        var category = await _categoryRepository.GetByIdAsync(createDemandDto.CategoryId);

        if (category is null)
            throw new InvalidOperationException("Categoria não encontrada.");

        var createdByUser = await _userRepository.GetByIdAsync(createDemandDto.CreatedByUserId);

        if (createdByUser is null)
            throw new InvalidOperationException("Usuário solicitante não encontrado.");

        if (createDemandDto.AssignedToUserId.HasValue)
        {
            var assignedUser = await _userRepository.GetByIdAsync(createDemandDto.AssignedToUserId.Value);

            if (assignedUser is null)
                throw new InvalidOperationException("Usuário responsável não encontrado.");
        }

        var demand = new Demand
        {
            Id = Guid.NewGuid(),
            Title = createDemandDto.Title,
            Description = createDemandDto.Description,
            Priority = createDemandDto.Priority,
            Status = DemandStatus.Open,
            CategoryId = createDemandDto.CategoryId,
            CreatedByUserId = createDemandDto.CreatedByUserId,
            AssignedToUserId = createDemandDto.AssignedToUserId,
            Notes = createDemandDto.Notes,
            CreatedAt = DateTime.UtcNow
        };

        var createdDemand = await _demandRepository.CreateAsync(demand);

        var demandWithRelations = await _demandRepository.GetByIdAsync(createdDemand.Id);

        return MapToDto(demandWithRelations!);
    }

    public async Task<DemandDto?> UpdateAsync(Guid id, UpdateDemandDto updateDemandDto)
    {
        var demand = await _demandRepository.GetByIdAsync(id);

        if (demand is null)
            return null;

        var category = await _categoryRepository.GetByIdAsync(updateDemandDto.CategoryId);

        if (category is null)
            throw new InvalidOperationException("Categoria não encontrada.");

        if (updateDemandDto.AssignedToUserId.HasValue)
        {
            var assignedUser = await _userRepository.GetByIdAsync(updateDemandDto.AssignedToUserId.Value);

            if (assignedUser is null)
                throw new InvalidOperationException("Usuário responsável não encontrado.");
        }

        demand.Title = updateDemandDto.Title;
        demand.Description = updateDemandDto.Description;
        demand.Priority = updateDemandDto.Priority;
        demand.Status = updateDemandDto.Status;
        demand.CategoryId = updateDemandDto.CategoryId;
        demand.AssignedToUserId = updateDemandDto.AssignedToUserId;
        demand.Notes = updateDemandDto.Notes;
        demand.UpdatedAt = DateTime.UtcNow;

        if (updateDemandDto.Status == DemandStatus.Completed && demand.CompletedAt is null)
            demand.CompletedAt = DateTime.UtcNow;

        if (updateDemandDto.Status != DemandStatus.Completed)
            demand.CompletedAt = null;

        var updatedDemand = await _demandRepository.UpdateAsync(demand);

        if (updatedDemand is null)
            return null;

        var demandWithRelations = await _demandRepository.GetByIdAsync(updatedDemand.Id);

        return MapToDto(demandWithRelations!);
    }

    public async Task<DemandDto?> UpdateStatusAsync(Guid id, UpdateDemandStatusDto updateDemandStatusDto)
    {
        var demand = await _demandRepository.GetByIdAsync(id);

        if (demand is null)
            return null;

        demand.Status = updateDemandStatusDto.Status;
        demand.UpdatedAt = DateTime.UtcNow;

        if (updateDemandStatusDto.Status == DemandStatus.Completed)
            demand.CompletedAt = DateTime.UtcNow;
        else
            demand.CompletedAt = null;

        var updatedDemand = await _demandRepository.UpdateAsync(demand);

        if (updatedDemand is null)
            return null;

        var demandWithRelations = await _demandRepository.GetByIdAsync(updatedDemand.Id);

        return MapToDto(demandWithRelations!);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        return await _demandRepository.DeleteAsync(id);
    }

    private static DemandDto MapToDto(Demand demand)
    {
        return new DemandDto
        {
            Id = demand.Id,
            Title = demand.Title,
            Description = demand.Description,
            Priority = demand.Priority,
            Status = demand.Status,
            CategoryId = demand.CategoryId,
            CategoryName = demand.Category?.Name,
            CreatedByUserId = demand.CreatedByUserId,
            CreatedByUserName = demand.CreatedByUser?.Name,
            AssignedToUserId = demand.AssignedToUserId,
            AssignedToUserName = demand.AssignedToUser?.Name,
            Notes = demand.Notes,
            CreatedAt = demand.CreatedAt,
            UpdatedAt = demand.UpdatedAt,
            CompletedAt = demand.CompletedAt
        };
    }
}