using Microsoft.EntityFrameworkCore;
using UserManagement.Api.Application.Interfaces;
using UserManagement.Api.Domain.Entities;
using UserManagement.Api.Infrastructure.Data;

namespace UserManagement.Api.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }


    public async Task<User?> GetByIdAsync(Guid id)
    {
        return await _context.Users
            .FirstOrDefaultAsync(x => x.Id == id);
    }


    public async Task<User?> GetByEmailAsync(string email)
    {
        email = email.Trim().ToLowerInvariant();

        return await _context.Users
            .FirstOrDefaultAsync(x => x.Email == email);
    }


    public async Task<List<User>> GetAllAsync()
    {
        return await _context.Users
            .AsNoTracking()
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();
    }


    public async Task<bool> ExistsByEmailAsync(
        string email,
        Guid? excludeUserId = null)
    {
        email = email.Trim().ToLowerInvariant();

        var query = _context.Users
            .AsQueryable();

        if (excludeUserId.HasValue)
        {
            query = query.Where(x =>
                x.Id != excludeUserId.Value);
        }

        return await query.AnyAsync(x =>
            x.Email == email);
    }


    public async Task AddAsync(User user)
    {
        await _context.Users.AddAsync(user);
    }


    public void Update(User user)
    {
        _context.Users.Update(user);
    }


    public void Delete(User user)
    {
        _context.Users.Remove(user);
    }


    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}