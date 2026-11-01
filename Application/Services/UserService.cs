using UserManagement.Api.Application.DTOs.Users;
using UserManagement.Api.Application.Interfaces;
using UserManagement.Api.Common.Exceptions;
using UserManagement.Api.Domain.Entities;

namespace UserManagement.Api.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(
        IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }


    public async Task<UserResponse> GetByIdAsync(Guid id)
    {
        var user = await _userRepository.GetByIdAsync(id);

        if (user is null)
        {
            throw new NotFoundException(
                "User not found.");
        }

        return Map(user);
    }


    public async Task<List<UserResponse>> GetAllAsync()
    {
        var users =
            await _userRepository.GetAllAsync();

        return users
            .Select(Map)
            .ToList();
    }


    public async Task<UserResponse> CreateAsync(
        CreateUserRequest request)
    {
        var exists =
            await _userRepository.ExistsByEmailAsync(
                request.Email);

        if (exists)
        {
            throw new BadRequestException(
                "Email is already registered.");
        }


        var passwordHash =
            BCrypt.Net.BCrypt.HashPassword(
                request.Password);


        var user = new User(
            request.FirstName,
            request.LastName,
            request.Email,
            passwordHash,
            request.Role);


        await _userRepository.AddAsync(user);

        await _userRepository.SaveChangesAsync();

        return Map(user);
    }


    public async Task<UserResponse> UpdateAsync(
        Guid id,
        UpdateUserRequest request)
    {
        var user =
            await _userRepository.GetByIdAsync(id);

        if (user is null)
        {
            throw new NotFoundException(
                "User not found.");
        }


        var emailExists =
            await _userRepository.ExistsByEmailAsync(
                request.Email,
                id);

        if (emailExists)
        {
            throw new BadRequestException(
                "Email is already registered.");
        }


        user.Update(
            request.FirstName,
            request.LastName,
            request.Email);


        await _userRepository.SaveChangesAsync();

        return Map(user);
    }


    public async Task DeleteAsync(Guid id)
    {
        var user =
            await _userRepository.GetByIdAsync(id);

        if (user is null)
        {
            throw new NotFoundException(
                "User not found.");
        }


        _userRepository.Delete(user);

        await _userRepository.SaveChangesAsync();
    }


    private static UserResponse Map(User user)
    {
        return new UserResponse
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            Role = user.Role,
            IsActive = user.IsActive,
            CreatedAt = user.CreatedAt,
            UpdatedAt = user.UpdatedAt
        };
    }
}