using FitPhoneBackend.Business.Entities;
using FitPhoneBackend.Business.Interfaces;
using FitPhoneBackend.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FitPhoneBackend.Business.Services
{
    public class UserService : ICreatable<User>, IReadable<User>, IUpdatable<User>, IDeletable<User>
    {
        private readonly ApplicationDbContext _context;

        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<User> CreateEntityAsync(User entity)
        {
            entity.Id = Guid.NewGuid();
            entity.CreatedAt = DateTime.UtcNow;
            entity.UpdatedAt = DateTime.UtcNow;

            _context.Users.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<List<User>> GetAllEntitiesAsync() =>
            await _context.Users.ToListAsync();

        public async Task<User?> GetEntityByIdAsync(Guid id) =>
            await _context.Users.FindAsync(id);

        public async Task<List<User>> GetEntitiesByConditionAsync(Expression<Func<User, bool>> predicate) =>
            await _context.Users.Where(predicate).ToListAsync();

        public async Task<User?> GetSingleEntityAsync(Expression<Func<User, bool>> predicate) =>
            await _context.Users.FirstOrDefaultAsync(predicate);

        public async Task<bool> UpdateEntityAsync(User entity)
        {
            var existing = await _context.Users.FindAsync(entity.Id);
            if (existing == null) return false;

            existing.Username = entity.Username;
            existing.Email = entity.Email;
            existing.Password = entity.Password;
            existing.UsageReason = entity.UsageReason;
            existing.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteEntityAsync(Guid id)
        {
            var existing = await _context.Users.FindAsync(id);
            if (existing == null) return false;

            _context.Users.Remove(existing);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
