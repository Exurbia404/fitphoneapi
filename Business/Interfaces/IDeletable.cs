using System;

namespace FitPhoneBackend.Business.Interfaces
{
    public interface IDeletable<T>
    {
        Task<bool> DeleteEntityAsync(Guid id);
    }
}
