using UserManagement.Api.Application.DTOs.Users;

namespace UserManagement.Api.Application.DTOs.Auth;

public class LoginResponse
{
    public string AccessToken { get; set; } = string.Empty;

    public DateTime ExpiresAt { get; set; }

    public UserResponse User { get; set; } = null!;
}