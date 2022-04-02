using ACOC.Barista.Models;
using System.Linq.Expressions;

namespace ACOC.Barista.Repositiories
{
    public interface IRepository<T>
    {
        Task<List<T>> GetAsync();
        Task<T?> GetAsync(string id);
        Task<IEnumerable<T>> GetByFilter(Expression<Func<T, bool>> filter);
        Task<T> GetFirstOrDefaultByFilter(Expression<Func<T, bool>> filter);
        Task CreateAsync(T newEntity);
        Task UpdateAsync(string id, T updatedEntity);
        Task RemoveAsync(string id);
    }
}
