using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FitPhoneBackend.Business.Interfaces
{
    public interface IReadable<T>
    {
        Task<List<T>> GetAllEntitiesAsync();
        Task<T?> GetEntityByIdAsync(Guid id);
        Task<List<T>> GetEntitiesByConditionAsync(Expression<Func<T, bool>> predicate);
        Task<T?> GetSingleEntityAsync(Expression<Func<T, bool>> predicate);
    }
}
