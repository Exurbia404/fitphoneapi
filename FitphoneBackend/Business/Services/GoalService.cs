using FitPhoneBackend.Business.Entities;
using FitPhoneBackend.Business.Interfaces;
using FitPhoneBackend.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FitPhoneBackend.Business.Services
{
    public class GoalService :
        ICreatable<Goal>,
        IReadable<Goal>,
        IUpdatable<Goal>,
        IDeletable<Goal>
    {
        private readonly ApplicationDbContext _context;

        public GoalService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Goal> CreateEntityAsync(Goal entity)
        {
            entity.Id = Guid.NewGuid();
            _context.Goals.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<List<Goal>> GetAllEntitiesAsync()
            => await _context.Goals.ToListAsync();

        public async Task<Goal?> GetEntityByIdAsync(Guid id)
            => await _context.Goals.FindAsync(id);

        public async Task<List<Goal>> GetEntitiesByConditionAsync(
            Expression<Func<Goal, bool>> predicate)
            => await _context.Goals.Where(predicate).ToListAsync();

        public async Task<Goal?> GetSingleEntityAsync(
            Expression<Func<Goal, bool>> predicate)
            => await _context.Goals.FirstOrDefaultAsync(predicate);

        public async Task<bool> UpdateEntityAsync(Goal entity)
        {
            var existing = await _context.Goals.FindAsync(entity.Id);
            if (existing == null) return false;

            existing.UserId = entity.UserId;
            existing.Name = entity.Name;
            existing.Description = entity.Description;
            existing.CurrentProgress = entity.CurrentProgress;
            existing.TargetGoal = entity.TargetGoal;
            existing.StartDate = entity.StartDate;
            existing.EndDate = entity.EndDate;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteEntityAsync(Guid id)
        {
            var existing = await _context.Goals.FindAsync(id);
            if (existing == null) return false;

            _context.Goals.Remove(existing);
            await _context.SaveChangesAsync();
            return true;
        }

        // Helper: Get all goals for a specific user
        public Task<List<Goal>> GetGoalsByUserIdAsync(Guid userId)
            => GetEntitiesByConditionAsync(g => g.UserId == userId);
    }
}