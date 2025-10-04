using DataAccessLayer.Context;
using DataAccessLayer.Entites.Identity;
using Microsoft.EntityFrameworkCore;
using RepositoriesLayer.Interfaces;
using Store.Repositories.Specification;

namespace RepositoriesLayer.Repositories
{
    public class GenericRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        private readonly ApplicationDbContext _context;
        public GenericRepository(ApplicationDbContext context)
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
            => await  _context.Set<TEntity>().FindAsync(id);

        public void Update(TEntity entity)
            => _context.Set<TEntity>().Update(entity);
        public async Task<TEntity> GetByIdWithSpecificationAsync(ISpecification<TEntity> specs)
            => await ApplySpecification(specs).FirstOrDefaultAsync();

        public async Task<IReadOnlyList<TEntity>> GetAllWithSpecificationAsync(ISpecification<TEntity> specs)
            => await ApplySpecification(specs).ToListAsync();
        private IQueryable<TEntity> ApplySpecification(ISpecification<TEntity> specs)
        {
            return SpecificationEvaluator<TKey, TEntity>.GetQuery(_context.Set<TEntity>(), specs);
        }
    }
}
