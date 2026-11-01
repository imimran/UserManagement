using Microsoft.EntityFrameworkCore;
using UserManagement.Api.Domain.Entities;
using UserManagement.Api.Domain.Enums;

namespace UserManagement.Api.Infrastructure.Data;

public static class DbSeeder
{
    public static async Task SeedAsync(
        AppDbContext context)
    {
        // Database migration apply
        await context.Database.MigrateAsync();

        // Admin already exists?
        var adminExists = await context.Users
            .AnyAsync(x => x.Role == UserRole.Admin);

        if (adminExists)
        {
            return;
        }

        // Create admin
        var admin = new User(
            firstName: "System",
            lastName: "Admin",
            email: "admin@mail.com",
            passwordHash: BCrypt.Net.BCrypt.HashPassword(
                "123456"),
            role: UserRole.Admin
        );

        await context.Users.AddAsync(admin);

        await context.SaveChangesAsync();
    }
}