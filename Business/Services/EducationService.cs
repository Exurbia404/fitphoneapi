// FitphoneBackend/Business/Services/EducationService.cs
using FitPhoneBackend.Business.Entities;
using FitPhoneBackend.Business.Interfaces;
using FitPhoneBackend.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FitPhoneBackend.Business.Services
{
    public class EducationService :
        ICreatable<Education>,
        IReadable<Education>,
        IUpdatable<Education>,
        IDeletable<Education>
    {
        private readonly ApplicationDbContext _context;

        public EducationService(ApplicationDbContext context)
        {
            _context = context;
        }

        // CREATE
        public async Task<Education> CreateEntityAsync(Education entity)
        {
            entity.Id = Guid.NewGuid();

            _context.Educations.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        // READ - All
        public async Task<List<Education>> GetAllEntitiesAsync()
            => await _context.Educations.ToListAsync();

        // READ - By Id (MUST match interface)
        public async Task<Education?> GetEntityByIdAsync(Guid id)
            => await _context.Educations.FindAsync(id);

        // READ - By Condition
        public async Task<List<Education>> GetEntitiesByConditionAsync(
            Expression<Func<Education, bool>> predicate)
            => await _context.Educations.Where(predicate).ToListAsync();

        // READ - Single
        public async Task<Education?> GetSingleEntityAsync(
            Expression<Func<Education, bool>> predicate)
            => await _context.Educations.FirstOrDefaultAsync(predicate);

        // UPDATE
        public async Task<bool> UpdateEntityAsync(Education entity)
        {
            var existing = await _context.Educations.FindAsync(entity.Id);
            if (existing == null) return false;

            existing.Name = entity.Name;
            existing.Description = entity.Description;
            existing.VideoURL = entity.VideoURL;
            existing.ArticleURL = entity.ArticleURL;

            await _context.SaveChangesAsync();
            return true;
        }

        // DELETE
        public async Task<bool> DeleteEntityAsync(Guid id)
        {
            var existing = await _context.Educations.FindAsync(id);
            if (existing == null) return false;

            _context.Educations.Remove(existing);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}