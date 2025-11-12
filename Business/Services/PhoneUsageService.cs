// FitPhoneBackend.Business/Services/PhoneUsageService.cs
using FitPhoneBackend.Business.Entities;
using FitPhoneBackend.Business.Interfaces;
using FitPhoneBackend.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FitPhoneBackend.Business.Services
{
    public class PhoneUsageService :
        ICreatable<PhoneUsage>,
        IReadable<PhoneUsage>,
        IUpdatable<PhoneUsage>,
        IDeletable<PhoneUsage>
    {
        private readonly ApplicationDbContext _context;

        public PhoneUsageService(ApplicationDbContext context)
        {
            _context = context;
        }

        // CREATE
        public async Task<PhoneUsage> CreateEntityAsync(PhoneUsage entity)
        {
            entity.Id = Guid.NewGuid();

            _context.PhoneUsages.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        // READ - All
        public async Task<List<PhoneUsage>> GetAllEntitiesAsync()
            => await _context.PhoneUsages.ToListAsync();

        // READ - By Id
        public async Task<PhoneUsage?> GetEntityByIdAsync(Guid id)
            => await _context.PhoneUsages.FindAsync(id);

        // READ - By Condition (e.g., by UserId)
        public async Task<List<PhoneUsage>> GetEntitiesByConditionAsync(
            Expression<Func<PhoneUsage, bool>> predicate)
            => await _context.PhoneUsages.Where(predicate).ToListAsync();

        // READ - Single (e.g., FirstOrDefault)
        public async Task<PhoneUsage?> GetSingleEntityAsync(
            Expression<Func<PhoneUsage, bool>> predicate)
            => await _context.PhoneUsages.FirstOrDefaultAsync(predicate);

        // UPDATE
        public async Task<bool> UpdateEntityAsync(PhoneUsage entity)
        {
            var existing = await _context.PhoneUsages.FindAsync(entity.Id);
            if (existing == null) return false;

            existing.UserId = entity.UserId;
            existing.ScreenTimeMinutes = entity.ScreenTimeMinutes;

            // Add any other fields you want to update
            await _context.SaveChangesAsync();
            return true;
        }

        // DELETE
        public async Task<bool> DeleteEntityAsync(Guid id)
        {
            var existing = await _context.PhoneUsages.FindAsync(id);
            if (existing == null) return false;

            _context.PhoneUsages.Remove(existing);
            await _context.SaveChangesAsync();
            return true;
        }

        // BONUS: Convenience method (not in interface)
        public Task<PhoneUsage?> GetPhoneUsageByUserIdAsync(Guid userId)
            => GetSingleEntityAsync(p => p.UserId == userId);
    }
}