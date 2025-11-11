using FitPhoneBackend.Business.Entities;
using FitPhoneBackend.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace FitPhoneBackend.Business.Services
{
    public interface IPhoneUsageService
    {
        Task<PhoneUsage?> GetPhoneUsageByUserIdAsync(Guid userId);
        Task<PhoneUsage?> GetPhoneUsageByIdAsync(Guid id);
        Task<IEnumerable<PhoneUsage>> GetAllPhoneUsageAsync();
        Task<PhoneUsage> CreatePhoneUsageAsync(PhoneUsage phoneUsage);
        Task UpdatePhoneUsageAsync(PhoneUsage phoneUsage);
        Task DeletePhoneUsageAsync(Guid id);
    }

    public class PhoneUsageService : IPhoneUsageService
    {
        private readonly ApplicationDbContext _dbContext;

        public PhoneUsageService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<PhoneUsage?> GetPhoneUsageByUserIdAsync(Guid userId)
        {
            return await _dbContext.PhoneUsages
                .FirstOrDefaultAsync(p => p.UserId == userId);
        }

        public async Task<PhoneUsage?> GetPhoneUsageByIdAsync(Guid id)
        {
            return await _dbContext.PhoneUsages.FindAsync(id);
        }

        public async Task<IEnumerable<PhoneUsage>> GetAllPhoneUsageAsync()
        {
            return await _dbContext.PhoneUsages.ToListAsync();
        }

        public async Task<PhoneUsage> CreatePhoneUsageAsync(PhoneUsage phoneUsage)
        {
            _dbContext.PhoneUsages.Add(phoneUsage);
            await _dbContext.SaveChangesAsync();
            return phoneUsage;
        }

        public async Task UpdatePhoneUsageAsync(PhoneUsage phoneUsage)
        {
            _dbContext.PhoneUsages.Update(phoneUsage);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeletePhoneUsageAsync(Guid id)
        {
            var entity = await _dbContext.PhoneUsages.FindAsync(id);
            if (entity != null)
            {
                _dbContext.PhoneUsages.Remove(entity);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
