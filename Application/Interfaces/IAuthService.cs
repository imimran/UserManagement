using UserManagement.Api.Application.DTOs.Auth;

namespace UserManagement.Api.Application.Interfaces;

public interface IAuthService
{
    Task<LoginResponse> RegisterAsync(
        RegisterRequest request);

    Task<LoginResponse> LoginAsync(
        LoginRequest request);
}