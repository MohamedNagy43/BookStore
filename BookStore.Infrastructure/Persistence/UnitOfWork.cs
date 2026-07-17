using BookStore.Infrastructure.Persistence.Repositories;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace BookStore.Infrastructure.Persistence;

public class UnitOfWork(ApplicationDbContext context) : IUnitOfWork
{
    private readonly Dictionary<Type, object> _repositories = [];
    private readonly ApplicationDbContext _context = context;

    public IRepository<TEntity, TKey> Repository<TEntity, TKey>()
        where TEntity : BaseEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        Type key = typeof(TEntity);

        if (_repositories.TryGetValue(key, out object? value))
            return (IRepository<TEntity, TKey>)value;

        var repository = new GenericRepository<TEntity, TKey>(_context);

        _repositories[key] = repository;

        return repository;
    }


    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }
}
