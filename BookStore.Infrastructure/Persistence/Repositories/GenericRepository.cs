using BookStore.Domain.Common;
using System.Linq.Expressions;

namespace BookStore.Infrastructure.Persistence.Repositories;

public class GenericRepository<TEntity, TKey>(ApplicationDbContext context)
    : IRepository<TEntity, TKey>
    where TEntity : BaseEntity<TKey>
    where TKey : IEquatable<TKey>

{
    private readonly ApplicationDbContext _context = context;
    private readonly IQueryable<TEntity> _query = context.Set<TEntity>().Where(x => !x.IsDeleted);

    public async Task<IEnumerable<TEntity>> GetAllAsync(ISpecification<TEntity>? specification = null, CancellationToken cancellationToken = default)
    {
        var query = SpecificationEvaluator.Evaluate<TEntity, TKey>(_query, specification);

        return await query.AsNoTracking().ToListAsync(cancellationToken);
    }
    public async Task<TEntity?> GetByIdAsync(TKey key, ISpecification<TEntity>? specification = null, bool includeDeleted = false, CancellationToken cancellationToken = default)
    {
        var query = _context.Set<TEntity>().Where(x => !x.IsDeleted || includeDeleted);

        return await SpecificationEvaluator.Evaluate<TEntity, TKey>(query, specification)
            .SingleOrDefaultAsync(x => x.Id.Equals(key), cancellationToken);
    }
    public async Task<TEntity?> GetAsync(ISpecification<TEntity>? specification = null,CancellationToken cancellationToken = default)
    {

        return await SpecificationEvaluator.Evaluate<TEntity, TKey>(_query, specification)
            .FirstOrDefaultAsync(cancellationToken);
    }
    public async Task<int> GetCountAsync(ISpecification<TEntity>? specification = null, CancellationToken cancellationToken = default)
    {
        return await SpecificationEvaluator.Evaluate<TEntity, TKey>(_query, specification)
            .CountAsync(cancellationToken);
    }
    public async Task<bool> ExistAsync(TKey key, CancellationToken cancellationToken = default)
    {
        return await _query.AnyAsync(x => x.Id.Equals(key), cancellationToken);
    }
    public async Task<bool> ExistAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default)
    {
        return await _query.AnyAsync(expression, cancellationToken);
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
