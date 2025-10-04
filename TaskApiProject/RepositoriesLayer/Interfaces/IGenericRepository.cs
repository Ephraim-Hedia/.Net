using DataAccessLayer.Entites.Identity;
using Store.Repositories.Specification;

namespace RepositoriesLayer.Interfaces
{
    public interface IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        Task<TEntity> GetByIdAsync(TKey? id);
        Task<IReadOnlyList<TEntity>> GetAllAsync();
        Task AddAsync(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        public Task<IReadOnlyList<TEntity>> GetAllWithSpecificationAsync(ISpecification<TEntity> specs);
        public Task<TEntity> GetByIdWithSpecificationAsync(ISpecification<TEntity> specs);
    }
}
