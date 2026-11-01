using UserManagement.Api.Domain.Entities;

namespace UserManagement.Api.Application.Interfaces;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(Guid id);

    Task<User?> GetByEmailAsync(string email);

    Task<List<User>> GetAllAsync();

    Task<bool> ExistsByEmailAsync(
        string email,
        Guid? excludeUserId = null);

    Task AddAsync(User user);

    void Update(User user);

    void Delete(User user);

    Task SaveChangesAsync();
}