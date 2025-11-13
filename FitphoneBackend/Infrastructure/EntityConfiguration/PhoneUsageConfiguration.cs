/*
These are configurations for migrations to be created automatically
*/

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FitPhoneBackend.Business.Entities;

public class PhoneUsageConfiguration : IEntityTypeConfiguration<PhoneUsage>
{
    public void Configure(EntityTypeBuilder<PhoneUsage> entity)
    {
            entity.ToTable("phoneusage");                  // Map to table "phoneUsage"
            entity.HasKey(pu => pu.Id);                 // Primary key

            entity.Property(pu => pu.UserId)
                  .IsRequired(); 

            entity.Property(pu => pu.UnlockCount)
                  .IsRequired();

            entity.Property(pu => pu.ScreenTimeMinutes)
                  .IsRequired();          
    }
}
