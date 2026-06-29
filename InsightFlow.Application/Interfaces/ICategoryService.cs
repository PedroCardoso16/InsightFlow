using InsightFlow.Application.DTOs;

namespace InsightFlow.Application.Interfaces;

public interface ICategoryService
{
    Task<List<CategoryDto>> GetAllAsync();

    Task<CategoryDto?> GetByIdAsync(Guid id);

    Task<CategoryDto> CreateAsync(CreateCategoryDto createCategoryDto);

    Task<CategoryDto?> UpdateAsync(Guid id, UpdateCategoryDto updateCategoryDto);

    Task<bool> DeleteAsync(Guid id);
}