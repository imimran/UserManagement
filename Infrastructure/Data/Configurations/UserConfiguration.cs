using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserManagement.Api.Domain.Entities;

namespace UserManagement.Api.Infrastructure.Data.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> entity)
    {
        // Table
        entity.ToTable("users");

        // Primary Key
        entity.HasKey(x => x.Id);

        // FirstName
        entity.Property(x => x.FirstName)
            .HasMaxLength(100)
            .IsRequired();

        // LastName
        entity.Property(x => x.LastName)
            .HasMaxLength(100)
            .IsRequired();

        // Email
        entity.Property(x => x.Email)
            .HasMaxLength(200)
            .IsRequired();

        entity.HasIndex(x => x.Email)
            .IsUnique();

        // PasswordHash
        entity.Property(x => x.PasswordHash)
            .HasMaxLength(500)
            .IsRequired();

        // Role
        entity.Property(x => x.Role)
            .HasConversion<string>()
            .HasMaxLength(20)
            .IsRequired();

        // IsActive
        entity.Property(x => x.IsActive)
            .IsRequired();

        // CreatedAt
        entity.Property(x => x.CreatedAt)
            .IsRequired();

        // UpdatedAt
        entity.Property(x => x.UpdatedAt)
            .IsRequired(false);
    }
}