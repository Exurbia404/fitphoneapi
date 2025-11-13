using FitPhoneBackend.Business.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FitPhoneBackend.Infrastructure.Configurations
{
    public class GoalConfiguration : IEntityTypeConfiguration<Goal>
    {
        public void Configure(EntityTypeBuilder<Goal> builder)
        {
            builder.ToTable("Goals");

            builder.HasKey(g => g.Id);

            builder.Property(g => g.UserId)
                   .IsRequired();

            builder.Property(g => g.Name)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(g => g.Description)
                   .HasMaxLength(1000);

            builder.Property(g => g.CurrentProgress)
                   .IsRequired();

            builder.Property(g => g.TargetGoal)
                   .IsRequired();

            builder.Property(g => g.StartDate)
                   .IsRequired();

            builder.Property(g => g.EndDate)
                   .IsRequired();
        }
    }
}