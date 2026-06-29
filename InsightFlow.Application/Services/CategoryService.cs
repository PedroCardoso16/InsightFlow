using InsightFlow.Application.DTOs;
using InsightFlow.Application.Interfaces;
using InsightFlow.Domain.Entities;

namespace InsightFlow.Application.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<List<CategoryDto>> GetAllAsync()
    {
        var categories = await _categoryRepository.GetAllAsync();

        return categories.Select(category => new CategoryDto
        {
            Id = category.Id,
            Name = category.Name,
            Description = category.Description,
            IsActive = category.IsActive,
            CreatedAt = category.CreatedAt
        }).ToList();
    }

    public async Task<CategoryDto?> GetByIdAsync(Guid id)
    {
        var category = await _categoryRepository.GetByIdAsync(id);

        if (category is null)
            return null;

        return new CategoryDto
        {
            Id = category.Id,
            Name = category.Name,
            Description = category.Description,
            IsActive = category.IsActive,
            CreatedAt = category.CreatedAt
        };
    }

    public async Task<CategoryDto> CreateAsync(CreateCategoryDto createCategoryDto)
    {
        var category = new Category
        {
            Id = Guid.NewGuid(),
            Name = createCategoryDto.Name,
            Description = createCategoryDto.Description,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        var createdCategory = await _categoryRepository.CreateAsync(category);

        return new CategoryDto
        {
            Id = createdCategory.Id,
            Name = createdCategory.Name,
            Description = createdCategory.Description,
            IsActive = createdCategory.IsActive,
            CreatedAt = createdCategory.CreatedAt
        };
    }

    public async Task<CategoryDto?> UpdateAsync(Guid id, UpdateCategoryDto updateCategoryDto)
    {
        var category = await _categoryRepository.GetByIdAsync(id);

        if (category is null)
            return null;

        category.Name = updateCategoryDto.Name;
        category.Description = updateCategoryDto.Description;
        category.IsActive = updateCategoryDto.IsActive;

        var updatedCategory = await _categoryRepository.UpdateAsync(category);

        if (updatedCategory is null)
            return null;

        return new CategoryDto
        {
            Id = updatedCategory.Id,
            Name = updatedCategory.Name,
            Description = updatedCategory.Description,
            IsActive = updatedCategory.IsActive,
            CreatedAt = updatedCategory.CreatedAt
        };
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        return await _categoryRepository.DeleteAsync(id);
    }
}