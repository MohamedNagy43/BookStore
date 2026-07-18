using System.Linq.Expressions;

namespace BookStore.Application.Abstractions.Specifications;

public class Specification<TEntity>(Expression<Func<TEntity, bool>>? criteria = null) : ISpecification<TEntity>
{
    public Expression<Func<TEntity, bool>>? Criteria { get; protected set; } = criteria;

    public List<Expression<Func<TEntity, object>>> Includes { get; protected set; } = [];

    public Expression<Func<TEntity, object>>? OrderBy { get; protected set; }

    public Expression<Func<TEntity, object>>? OrderByDescending { get; protected set; }

    public int Take { get; protected set; }

    public int Skip { get; protected set; }

    public bool IsPagingEnabled { get; protected set; }

    protected void ApplyPaging(int pageNumber, int pageSize)
    {
        if (pageNumber <= 0 || pageSize <= 0)
            throw new InvalidOperationException("Can not apply pagination");

        Take = pageSize;
        Skip = (pageNumber - 1) * pageSize;
    }
    protected void AddIncludes(params Expression<Func<TEntity, object>>[] includes)
    {
        Includes.AddRange(includes);
    }
    protected void ApplyOrderBy(Expression<Func<TEntity, object>> orderBy)
    {
        if (OrderByDescending is not null)
            throw new InvalidOperationException("Only one ordering expression is allowed.");

        OrderBy = orderBy;
    }
    protected void ApplyOrderByDescending(Expression<Func<TEntity, object>> orderByDescending)
    {
        if (OrderBy is not null)
            throw new InvalidOperationException("Only one ordering expression is allowed.");

        OrderByDescending = orderByDescending;
    }


}
