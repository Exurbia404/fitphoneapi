/*
These are configurations for migrations to be created automatically
*/

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FitPhoneBackend.Business.Entities;
public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> entity)
    {
        entity.ToTable("users");                  // Map to table "users"
        entity.HasKey(u => u.Id);                 // Primary key

        entity.Property(u => u.Email)
              .IsRequired();
        entity.HasIndex(u => u.Email).IsUnique(); 

        entity.Property(u => u.Username)
              .IsRequired()
              .HasMaxLength(50);
        entity.HasIndex(u => u.Username).IsUnique();

        entity.Property(u => u.Password)
              .IsRequired()
              .HasMaxLength(255);

        entity.HasOne(u => u.PhoneUsage)
            .WithOne()
            .HasForeignKey<PhoneUsage>(p => p.UserId)
            .IsRequired();

        entity.Property(p => p.CreatedAt)
              .HasDefaultValueSql("NOW()");      // default timestamp
    }
}
