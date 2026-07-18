using BookStore.Domain.Entities.Common;

namespace BookStore.Infrastructure.Persistence.Repositories;

public class GenericRepository<TEntity, TKey>(ApplicationDbContext context)
    : IRepository<TEntity, TKey>
    where TEntity : BaseEntity<TKey>
    where TKey : IEquatable<TKey>

{
    private readonly ApplicationDbContext _context = context;
    public IQueryable<TEntity> Query => _context.Set<TEntity>().Where(x => !x.IsDeleted);

    public async Task<IEnumerable<TEntity>> GetAllAsync(ISpecification<TEntity>? specification = null, CancellationToken cancellationToken = default)
    {
        var query = SpecificationEvaluator.Evaluate<TEntity, TKey>(Query, specification);

        return await query.AsNoTracking().ToListAsync(cancellationToken);
    }
    public async Task<TEntity?> GetByIdAsync(TKey key, ISpecification<TEntity>? specification = null, CancellationToken cancellationToken = default)
    {
        return await SpecificationEvaluator.Evaluate<TEntity, TKey>(Query, specification)
            .SingleOrDefaultAsync(x => x.Id.Equals(key), cancellationToken);
    }
    public async Task<TEntity?> GetAsync(ISpecification<TEntity>? specification = null, CancellationToken cancellationToken = default)
    {
        return await SpecificationEvaluator.Evaluate<TEntity, TKey>(Query, specification)
            .FirstOrDefaultAsync(cancellationToken);
    }
    public async Task<int> GetCountAsync(ISpecification<TEntity>? specification = null, CancellationToken cancellationToken = default)
    {
        return await SpecificationEvaluator.Evaluate<TEntity, TKey>(Query, specification)
            .CountAsync(cancellationToken);
    }
    public async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await _context.AddAsync(entity, cancellationToken);
    }
    public void SoftDelete(TEntity entity)
    {
        if (entity.IsDeleted)
            throw new InvalidOperationException("Entity is already deleted");

        entity.IsDeleted = true;
    }
    public void Restore(TEntity entity)
    {
        if (!entity.IsDeleted)
            throw new InvalidOperationException("entity is already active");

        entity.IsDeleted = false;
    }
}
