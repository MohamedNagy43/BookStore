using BookStore.Domain.Entities.Common;

namespace BookStore.Application.Abstractions.Repositories;

public interface IUnitOfWork
{
    IRepository<TEntity, TKey> Repository<TEntity, TKey>()
        where TEntity : BaseEntity<TKey>
        where TKey : IEquatable<TKey>;

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
