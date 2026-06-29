using InsightFlow.Domain.Entities;

namespace InsightFlow.Application.Interfaces;

public interface ICategoryRepository
{
    Task<List<Category>> GetAllAsync();

    Task<Category?> GetByIdAsync(Guid id);

    Task<Category> CreateAsync(Category category);

    Task<Category?> UpdateAsync(Category category);

    Task<bool> DeleteAsync(Guid id);
}