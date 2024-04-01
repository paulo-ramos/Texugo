using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Texugo.Login.Core.Contexts.AccountContext.Entities;

namespace Texugo.Login.Infraestrutura.Contexts.AccountContext.Mappings;

public class UserMap : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("User");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .HasColumnName("Name")
            .HasColumnType("VARCHAR")
            .HasMaxLength(120)
            .IsRequired(true);
        
        builder.Property(x => x.Image)
            .HasColumnName("Image")
            .HasColumnType("VARCHAR")
            .HasMaxLength(255)
            .IsRequired(true);
        
        builder.OwnsOne(x => x.Email)
            .Property(x => x.Address)
            .HasColumnName("Email")
            .HasColumnType("VARCHAR")
            .HasMaxLength(120)
            .IsRequired(true);
        
        builder.OwnsOne(x => x.Email)
            .OwnsOne(x => x.Verification)
            .Property(x => x.Code)
            .HasColumnName("EmailVerificationCode")
            .HasColumnType("VARCHAR")
            .HasMaxLength(8)
            .IsRequired(true);
        
        builder.OwnsOne(x => x.Email)
            .OwnsOne(x => x.Verification)
            .Property(x => x.ExpiresAt)
            .HasColumnName("EmailVerificationExpireAt")
            .HasColumnType("DATE")
            .IsRequired(false);
        
        builder.OwnsOne(x => x.Email)
            .OwnsOne(x => x.Verification)
            .Property(x => x.VerifiedAt)
            .HasColumnName("EmailVerificationVerifiedAt")
            .HasColumnType("DATE")
            .IsRequired(false);
        
        builder.OwnsOne(x => x.Email)
            .OwnsOne(x => x.Verification)
            .Ignore(x => x.IsActive);
        
        builder.OwnsOne(x => x.Password)
            .Property(x => x.Hash)
            .HasColumnName("PasswordHash")
            .HasColumnType("VARCHAR")
            .HasMaxLength(120)
            .IsRequired(true);
        
        builder.OwnsOne(x => x.Password)
            .Property(x => x.ResetCode)
            .HasColumnName("PasswordResetCode")
            .HasColumnType("VARCHAR")
            .HasMaxLength(8)
            .IsRequired(true);

        builder
            .HasMany(x => x.Roles)
            .WithMany(x => x.Users)
            .UsingEntity<Dictionary<string, object>>("UserRole",
                role => role.HasOne<Role>().WithMany().HasForeignKey("RoleId").OnDelete(DeleteBehavior.Cascade),
                user => user.HasOne<User>().WithMany().HasForeignKey("UserId").OnDelete(DeleteBehavior.Cascade));

    }
}