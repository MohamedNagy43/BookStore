using BookStore.Domain.Common;

namespace BookStore.Infrastructure.Persistence.Repositories;

public static class SpecificationEvaluator
{
    public static IQueryable<TEntity> Evaluate<TEntity, TKey>(IQueryable<TEntity> query, ISpecification<TEntity>? specification)
        where TEntity : BaseEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        if (specification == null)
            return query;

        if (specification.Criteria is not null)
            query = query.Where(specification.Criteria);

        if (specification.Includes.Count != 0)
            specification.Includes.ForEach(include =>
            {
                query = query.Include(include);
            });

        if(specification.OrderBy is not null)
            query = query.OrderBy(specification.OrderBy);
        else if(specification.OrderByDescending is not null)
            query = query.OrderByDescending(specification.OrderByDescending);

        if (specification.IsPagingEnabled)
            query = query.Skip(specification.Skip).Take(specification.Take);


        return query;
    }
}
