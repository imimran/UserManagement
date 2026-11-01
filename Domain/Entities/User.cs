using UserManagement.Api.Domain.Enums;

namespace UserManagement.Api.Domain.Entities;

public class User
{
    public Guid Id { get; private set; }

    public string FirstName { get; private set; } = null!;

    public string LastName { get; private set; } = null!;

    public string Email { get; private set; } = null!;

    public string PasswordHash { get; private set; } = null!;

    public UserRole Role { get; private set; }

    public bool IsActive { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public DateTime? UpdatedAt { get; private set; }


    // EF Core
    private User()
    {
    }


    public User(
        string firstName,
        string lastName,
        string email,
        string passwordHash,
        UserRole role = UserRole.User)
    {
        Id = Guid.NewGuid();

        FirstName = firstName.Trim();

        LastName = lastName.Trim();

        Email = email.Trim().ToLowerInvariant();

        PasswordHash = passwordHash;

        Role = role;

        IsActive = true;

        CreatedAt = DateTime.UtcNow;
    }


    public void Update(
        string firstName,
        string lastName,
        string email)
    {
        FirstName = firstName.Trim();

        LastName = lastName.Trim();

        Email = email.Trim().ToLowerInvariant();

        UpdatedAt = DateTime.UtcNow;
    }


    public void ChangePassword(string passwordHash)
    {
        PasswordHash = passwordHash;

        UpdatedAt = DateTime.UtcNow;
    }


    public void ChangeRole(UserRole role)
    {
        Role = role;

        UpdatedAt = DateTime.UtcNow;
    }


    public void Activate()
    {
        IsActive = true;

        UpdatedAt = DateTime.UtcNow;
    }


    public void Deactivate()
    {
        IsActive = false;

        UpdatedAt = DateTime.UtcNow;
    }
}