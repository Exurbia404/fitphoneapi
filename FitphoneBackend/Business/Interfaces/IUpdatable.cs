namespace FitPhoneBackend.Business.Interfaces
{
    public interface IUpdatable<T>
    {
        Task<bool> UpdateEntityAsync(T entity);
    }
}
