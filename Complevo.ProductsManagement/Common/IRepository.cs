using System.Linq.Expressions;

namespace Complevo.ProductsManagement.Common
{
    public interface IRepository<T> where T : IEntity
    {
        Task<IReadOnlyCollection<T>> GetAllAsync();
        Task<T> GetAsync(Guid id);
        Task<T> GetAsync(Expression<Func<T, bool>> filter);
        Task CreateAsync(T entity);
        Task UpdateAsync(T entity);
        Task RemoveAsync(Guid id);
    }
}
