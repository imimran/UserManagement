using UserManagement.Api.Domain.Entities;

namespace UserManagement.Api.Application.Interfaces;

public interface IJwtService
{
    string GenerateToken(
        User user,
        out DateTime expiresAt);
}