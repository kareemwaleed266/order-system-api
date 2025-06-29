using Order.Data.Context;
using Order.Data.Entities;
using Order.Repository.Interfaces;
using System.Collections;

namespace Order.Repository.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private Hashtable _repositories;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }
        public async Task<int> CompleteAsync()
        => await _context.SaveChangesAsync();

        public IGenericRepository<TEntity, Tkey> Repository<TEntity, Tkey>() where TEntity : BaseEntity<Tkey>
        {
            if (_repositories == null)
                _repositories = new Hashtable();

            var entityKey = typeof(TEntity).Name;

            if (!_repositories.ContainsKey(entityKey))
            {
                var repositoryType = typeof(GenericRepository<,>);
                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity), typeof(Tkey)), _context);
                _repositories.Add(entityKey, repositoryInstance);
            }

            return ((IGenericRepository<TEntity, Tkey>)_repositories[entityKey]);
        }
    }
}
