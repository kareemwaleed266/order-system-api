using Order.Data.Entities;
using System.Linq.Expressions;

namespace Order.Repository.Interfaces
{
    public interface IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        Task AddAsync(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);

        Task<IReadOnlyList<TEntity>> GetAllAsync();
        Task<TEntity> GetByIdAsync(TKey? id);
        Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> GetIfExistsAsync(Expression<Func<TEntity, bool>> predicate);
        //Task<Customer> GetByEmailAsync(string? Email);
    }
}
