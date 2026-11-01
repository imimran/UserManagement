using Microsoft.EntityFrameworkCore;
using UserManagement.Api.Application.Interfaces;
using UserManagement.Api.Application.Services;
using UserManagement.Api.Infrastructure.Data;
using UserManagement.Api.Infrastructure.Repositories;
using UserManagement.Api.Infrastructure.Security;

namespace UserManagement.Api.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddApplicationServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString =
            configuration.GetConnectionString(
                "DefaultConnection");


        services.AddDbContext<AppDbContext>(
            options =>
            {
                options.UseNpgsql(
                    connectionString);
            });


        services.AddScoped<
            IUserRepository,
            UserRepository>();


        services.AddScoped<
            IUserService,
            UserService>();


        services.AddScoped<
            IAuthService,
            AuthService>();


        services.AddScoped<
            IJwtService,
            JwtService>();


        return services;
    }
}