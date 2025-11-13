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

        public async Task<PhoneUsage> CreateEntityAsync(PhoneUsage entity)
        {
            entity.Id = Guid.NewGuid();

            _context.PhoneUsages.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<List<PhoneUsage>> GetAllEntitiesAsync()
            => await _context.PhoneUsages.ToListAsync();

        public async Task<PhoneUsage?> GetEntityByIdAsync(Guid id)
            => await _context.PhoneUsages.FindAsync(id);

        public async Task<List<PhoneUsage>> GetEntitiesByConditionAsync(
            Expression<Func<PhoneUsage, bool>> predicate)
            => await _context.PhoneUsages.Where(predicate).ToListAsync();

        public async Task<PhoneUsage?> GetSingleEntityAsync(
            Expression<Func<PhoneUsage, bool>> predicate)
            => await _context.PhoneUsages.FirstOrDefaultAsync(predicate);

        public async Task<bool> UpdateEntityAsync(PhoneUsage entity)
        {
            var existing = await _context.PhoneUsages.FindAsync(entity.Id);
            if (existing == null) return false;

            existing.UserId = entity.UserId;
            existing.ScreenTimeMinutes = entity.ScreenTimeMinutes;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteEntityAsync(Guid id)
        {
            var existing = await _context.PhoneUsages.FindAsync(id);
            if (existing == null) return false;

            _context.PhoneUsages.Remove(existing);
            await _context.SaveChangesAsync();
            return true;
        }

        public Task<PhoneUsage?> GetPhoneUsageByUserIdAsync(Guid userId)
            => GetSingleEntityAsync(p => p.UserId == userId);
    }
}