using Microsoft.EntityFrameworkCore;
using FitPhoneBackend.Business.Entities;
using FitPhoneBackend.Business.Services;

namespace FitPhoneBackend.Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        
            public DbSet<User> Users { get; set; }
            public DbSet<PhoneUsage> PhoneUsages { get; set; }
            public DbSet<Education> Educations{ get; set; }
            public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { } //in case I want to have extra options on this class
            
            public DbSet<Challenge> Challenges { get; set; }
            public DbSet<Goal> Goals { get; set;  }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Facades for user configurations
            modelBuilder.ApplyConfiguration(new UserConfiguration()); //Facades to decrease pollution in this config class
            modelBuilder.ApplyConfiguration(new PhoneUsageConfiguration());
            modelBuilder.ApplyConfiguration(new EducationConfiguration());
            modelBuilder.Entity<User>()
                .Property(u => u.CreatedAt)
                .HasColumnType("datetime(6)")
                .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");

            modelBuilder.Entity<User>()
                .Property(u => u.UpdatedAt)
                .HasColumnType("datetime(6)")
                .HasDefaultValueSql("CURRENT_TIMESTAMP(6) ON UPDATE CURRENT_TIMESTAMP(6)");
        }
    }
}
