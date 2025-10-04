using DataAccessLayer.Context;
using DataAccessLayer.Entites.Identity;
using RepositoriesLayer.Interfaces;
using System.Collections;

namespace RepositoriesLayer.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private Hashtable _repositories;
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }
        public IGenericRepository<TEntity, TKey> Repository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
        {
            if (_repositories == null)
                _repositories = new Hashtable();

            var entityKey = typeof(TEntity).Name;   // Repository<Product , int>

            if (!_repositories.ContainsKey(entityKey))
            {
                var repositoryType = typeof(GenericRepository<,>);
                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity), typeof(TKey)), _context);
                _repositories.Add(entityKey, repositoryInstance);

            }
            return (IGenericRepository<TEntity, TKey>)_repositories[entityKey];
        }

        public Task<int> SaveChangesAsync()
            => _context.SaveChangesAsync();
    }
}
