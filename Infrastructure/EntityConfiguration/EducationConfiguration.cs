/*
These are configurations for migrations to be created automatically
*/

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FitPhoneBackend.Business.Entities;
public class EducationConfiguration : IEntityTypeConfiguration<Education>
{
    public void Configure(EntityTypeBuilder<Education> entity)
    {
            entity.ToTable("education_material");   // Map to table "education_material"
            entity.HasKey(em => em.Id);                 // Primary key

            entity.Property(em => em.Name)
                  .IsRequired(); 

            entity.Property(em => em.Description)
                  .IsRequired()
                  .HasMaxLength(254);

            entity.Property(em => em.Type)
              .IsRequired();

            entity.Property(em => em.ArticleURL);          
            entity.Property(em => em.VideoURL);          
    }
}
