using BookStore.Domain.Entities.Common;

namespace BookStore.Application.Abstractions.Repositories;

public interface IRepository<TEntity, TKey>
    where TEntity : BaseEntity<TKey>
    where TKey : IEquatable<TKey>
{
    public IQueryable<TEntity> Query { get; }

    Task<IEnumerable<TEntity>> GetAllAsync(ISpecification<TEntity>? specification = null, CancellationToken cancellationToken = default);
    Task<TEntity?> GetByIdAsync(TKey key, ISpecification<TEntity>? specification = null, CancellationToken cancellationToken = default);
    Task<TEntity?> GetAsync(ISpecification<TEntity>? specification = null, CancellationToken cancellationToken = default);
    Task<int> GetCountAsync(ISpecification<TEntity>? specification = null, CancellationToken cancellationToken = default);
    Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);
    void SoftDelete(TEntity entity);
    void Restore(TEntity entity);
}
