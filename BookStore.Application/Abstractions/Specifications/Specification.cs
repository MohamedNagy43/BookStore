using BookStore.Shared.Constants;
using System.Linq.Expressions;

namespace BookStore.Application.Abstractions.Specifications;

public class Specification<TEntity>(Expression<Func<TEntity, bool>>? criteria = null) : ISpecification<TEntity>
{
    public Expression<Func<TEntity, bool>>? Criteria { get; private set; } = criteria;

    public List<Expression<Func<TEntity, object>>> Includes { get; private set; } = [];

    public Expression<Func<TEntity, object>>? OrderBy { get; private set; }

    public Expression<Func<TEntity, object>>? OrderByDescending { get; private set; }

    public int Take { get; private set; }

    public int Skip { get; private set; }

    public bool IsPagingEnabled { get; private set; }

    protected void ApplyPaging(int pageNumber, int pageSize)
    {
        if (pageNumber <= 0 || pageSize <= 0 || pageSize > PaginationSettings.MaxPageSize)
            throw new InvalidOperationException("Can not apply pagination");

        Take = pageSize;
        Skip = (pageNumber - 1) * pageSize;

        IsPagingEnabled = true;
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
