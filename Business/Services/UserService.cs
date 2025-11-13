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
            try
            {
                entity.Id = Guid.NewGuid();
                entity.CreatedAt = DateTime.UtcNow;
                entity.UpdatedAt = DateTime.UtcNow;

                _context.Users.Add(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[CreateEntityAsync] Error creating user: {ex}");
                throw new Exception("An error occurred while creating the user.", ex);
            }
        }

        public async Task<List<User>> GetAllEntitiesAsync()
        {
            try
            {
                return await _context.Users.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[GetAllEntitiesAsync] Error retrieving users: {ex}");
                throw new Exception("An error occurred while retrieving users.", ex);
            }
        }

        public async Task<User?> GetEntityByIdAsync(Guid id)
        {
            try
            {
                return await _context.Users.FindAsync(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[GetEntityByIdAsync] Error retrieving user with ID {id}: {ex}");
                throw new Exception($"An error occurred while retrieving the user with ID {id}.", ex);
            }
        }

        public async Task<List<User>> GetEntitiesByConditionAsync(Expression<Func<User, bool>> predicate)
        {
            try
            {
                return await _context.Users.Where(predicate).ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[GetEntitiesByConditionAsync] Error filtering users: {ex}");
                throw new Exception("An error occurred while filtering users.", ex);
            }
        }

        public async Task<User?> GetSingleEntityAsync(Expression<Func<User, bool>> predicate)
        {
            try
            {
                return await _context.Users.FirstOrDefaultAsync(predicate);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[GetSingleEntityAsync] Error retrieving single user: {ex}");
                throw new Exception("An error occurred while retrieving a single user.", ex);
            }
        }

        public async Task<bool> UpdateEntityAsync(User entity)
        {
            try
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
            catch (Exception ex)
            {
                Console.WriteLine($"[UpdateEntityAsync] Error updating user {entity.Id}: {ex}");
                throw new Exception($"An error occurred while updating user with ID {entity.Id}.", ex);
            }
        }

        public async Task<bool> DeleteEntityAsync(Guid id)
        {
            try
            {
                var existing = await _context.Users.FindAsync(id);
                if (existing == null) return false;

                _context.Users.Remove(existing);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[DeleteEntityAsync] Error deleting user {id}: {ex}");
                throw new Exception($"An error occurred while deleting user with ID {id}.", ex);
            }
        }
    }
}
