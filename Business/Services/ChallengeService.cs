using FitPhoneBackend.Business.Entities;
using FitPhoneBackend.Business.Interfaces;
using FitPhoneBackend.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FitPhoneBackend.Business.Services
{
    public class ChallengeService :
        ICreatable<Challenge>,
        IReadable<Challenge>,
        IUpdatable<Challenge>,
        IDeletable<Challenge>
    {
        private readonly ApplicationDbContext _context;

        public ChallengeService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Challenge> CreateEntityAsync(Challenge entity)
        {
            entity.Id = Guid.NewGuid();
            _context.Challenges.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<List<Challenge>> GetAllEntitiesAsync()
            => await _context.Challenges.ToListAsync();

        public async Task<Challenge?> GetEntityByIdAsync(Guid id)
            => await _context.Challenges.FindAsync(id);

        public async Task<List<Challenge>> GetEntitiesByConditionAsync(
            Expression<Func<Challenge, bool>> predicate)
            => await _context.Challenges.Where(predicate).ToListAsync();

        public async Task<Challenge?> GetSingleEntityAsync(
            Expression<Func<Challenge, bool>> predicate)
            => await _context.Challenges.FirstOrDefaultAsync(predicate);

        public async Task<bool> UpdateEntityAsync(Challenge entity)
        {
            var existing = await _context.Challenges.FindAsync(entity.Id);
            if (existing == null) return false;

            existing.UserId = entity.UserId;
            existing.Name = entity.Name;
            existing.Description = entity.Description;
            existing.CurrentProgress = entity.CurrentProgress;
            existing.TargetGoal = entity.TargetGoal;
            existing.StartDate = entity.StartDate;
            existing.EndDate = entity.EndDate;
            existing.MissionExplanation = entity.MissionExplanation;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteEntityAsync(Guid id)
        {
            var existing = await _context.Challenges.FindAsync(id);
            if (existing == null) return false;

            _context.Challenges.Remove(existing);
            await _context.SaveChangesAsync();
            return true;
        }

        // Helper: Get all challenges for a specific user
        public Task<List<Challenge>> GetChallengesByUserIdAsync(Guid userId)
            => GetEntitiesByConditionAsync(c => c.UserId == userId);
    }
}