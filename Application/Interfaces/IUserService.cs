using UserManagement.Api.Application.DTOs.Users;

namespace UserManagement.Api.Application.Interfaces;

public interface IUserService
{
    Task<UserResponse> GetByIdAsync(Guid id);

    Task<List<UserResponse>> GetAllAsync();

    Task<UserResponse> CreateAsync(
        CreateUserRequest request);

    Task<UserResponse> UpdateAsync(
        Guid id,
        UpdateUserRequest request);

    Task DeleteAsync(Guid id);
}