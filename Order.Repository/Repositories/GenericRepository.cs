using Microsoft.EntityFrameworkCore;
using Order.Data.Context;
using Order.Data.Entities;
using Order.Repository.Interfaces;
using System.Linq.Expressions;

namespace Order.Repository.Repositories
{
    public class GenericRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        private readonly AppDbContext _context;
        public GenericRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(TEntity entity)
            => await _context.Set<TEntity>().AddAsync(entity);

        public void Delete(TEntity entity)
            => _context.Set<TEntity>().Remove(entity);

        public async Task<IReadOnlyList<TEntity>> GetAllAsync()
            => await _context.Set<TEntity>().ToListAsync();

        public async Task<TEntity> GetByIdAsync(TKey? id)
            => await _context.Set<TEntity>().FindAsync(id);

        public void Update(TEntity entity)
            => _context.Set<TEntity>().Update(entity);

        public async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate)
            => await _context.Set<TEntity>().AnyAsync(predicate);

        //public async Task<Customer> GetByEmailAsync(string? Email)
        //{
        //    var customers = await _context.Customers
        //                .Where(o => o.Email.Contains(Email))
        //                .ToListAsync();

        //    var customer = await _unitOfWork.Repository<Customer,Guid>().GetByIdAsync(customers[0].Id);

        //    return customer;
        //}

        public async Task<TEntity> GetIfExistsAsync(Expression<Func<TEntity, bool>> predicate)
        => await _context.Set<TEntity>().FirstOrDefaultAsync(predicate);
    }
}
