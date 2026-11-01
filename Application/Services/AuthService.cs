using UserManagement.Api.Application.DTOs.Auth;
using UserManagement.Api.Application.DTOs.Users;
using UserManagement.Api.Application.Interfaces;
using UserManagement.Api.Common.Exceptions;
using UserManagement.Api.Domain.Entities;

namespace UserManagement.Api.Application.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;

    private readonly IJwtService _jwtService;


    public AuthService(
        IUserRepository userRepository,
        IJwtService jwtService)
    {
        _userRepository = userRepository;

        _jwtService = jwtService;
    }


    public async Task<LoginResponse> RegisterAsync(
        RegisterRequest request)
    {
        var emailExists =
            await _userRepository.ExistsByEmailAsync(
                request.Email);

        if (emailExists)
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
            passwordHash);


        await _userRepository.AddAsync(user);

        await _userRepository.SaveChangesAsync();


        var token =
            _jwtService.GenerateToken(
                user,
                out var expiresAt);


        return new LoginResponse
        {
            AccessToken = token,
            ExpiresAt = expiresAt,
            User = Map(user)
        };
    }


    public async Task<LoginResponse> LoginAsync(
        LoginRequest request)
    {
        var user =
            await _userRepository.GetByEmailAsync(
                request.Email);


        if (user is null)
        {
            throw new BadRequestException(
                "Invalid email or password.");
        }


        if (!user.IsActive)
        {
            throw new BadRequestException(
                "User account is inactive.");
        }


        var validPassword =
            BCrypt.Net.BCrypt.Verify(
                request.Password,
                user.PasswordHash);


        if (!validPassword)
        {
            throw new BadRequestException(
                "Invalid email or password.");
        }


        var token =
            _jwtService.GenerateToken(
                user,
                out var expiresAt);


        return new LoginResponse
        {
            AccessToken = token,
            ExpiresAt = expiresAt,
            User = Map(user)
        };
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