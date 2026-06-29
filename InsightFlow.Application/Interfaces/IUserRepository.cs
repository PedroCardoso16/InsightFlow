using InsightFlow.Domain.Entities;

namespace InsightFlow.Application.Interfaces;

public interface IUserRepository
{
    Task<List<User>> GetAllAsync();

    Task<User?> GetByIdAsync(Guid id);

    Task<User?> GetByEmailAsync(string email);

    Task<User> CreateAsync(User user);

    Task<User?> UpdateAsync(User user);

    Task<bool> DeleteAsync(Guid id);
}