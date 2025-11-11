using System.Threading.Tasks;

namespace FitPhoneBackend.Business.Interfaces
{
    public interface ICreatable<T>
    {
        Task<T> CreateEntityAsync(T entity);
    }
}
