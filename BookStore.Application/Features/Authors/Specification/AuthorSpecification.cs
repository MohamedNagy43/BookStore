using BookStore.Application.Common.Contracts;
using BookStore.Domain.Entities.Authors;
using System.Linq.Expressions;

namespace BookStore.Application.Features.Authors.Specification;

internal class AuthorSpecification : Specification<Author>, ISpecification<Author>
{
    private static Expression<Func<Author, bool>>? ApplyCriteria(string? searchValue)
    {
        if (string.IsNullOrEmpty(searchValue))
            return null;

        return x => x.FirstName.Contains(searchValue);
    }
    private void ApplyIncludes()
    {
        base.AddIncludes(x => x.File);
    }
    private void ApplySorting(string? sortColumn, string? sortDirction)
    {
        if (string.IsNullOrWhiteSpace(sortColumn))
            return;

        Expression<Func<Author, object>> expression = sortColumn.ToLower() switch
        {
            "firstname" => x => x.FirstName,
            "lastname" => x => x.LastName,
            _ => x => x.Id
        };

        if (sortDirction is null || sortDirction.Equals("ASC", StringComparison.OrdinalIgnoreCase))
            ApplyOrderBy(expression);
        else
            ApplyOrderByDescending(expression);
    }
    public AuthorSpecification(RequestFilters requestFilters)
        : base(ApplyCriteria(requestFilters.SearchValue))
    {
        ApplyIncludes();
        base.ApplyPaging(requestFilters.PageNumber, requestFilters.PageSize);
        ApplySorting(requestFilters.SortColumn, requestFilters.SortDirction);
    }
    public AuthorSpecification()
    {
        ApplyIncludes();
    }
}
