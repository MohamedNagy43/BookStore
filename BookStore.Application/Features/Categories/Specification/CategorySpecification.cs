using BookStore.Application.Common.Contracts;
using BookStore.Domain.Entities.Authors;
using BookStore.Domain.Entities.Categories;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace BookStore.Application.Features.Categories.Specification;

public class CategorySpecification : Specification<Category>, ISpecification<Category>
{
    private static Expression<Func<Category, bool>>? ApplyCriteria(string? searchValue)
    {
        if (string.IsNullOrEmpty(searchValue))
            return null;

        return x => x.Name.Contains(searchValue);
    }
    private void ApplySorting(string? sortColumn, string? sortDirction)
    {
        if (string.IsNullOrWhiteSpace(sortColumn))
            return;

        Expression<Func<Category, object>> expression = sortColumn.ToLower() switch
        {
            "name" => x => x.Name,
            _ => x => x.Id
        };

        if (sortDirction is null || sortDirction.Equals("ASC", StringComparison.OrdinalIgnoreCase))
            ApplyOrderBy(expression);
        else
            ApplyOrderByDescending(expression);
    }
    public CategorySpecification(RequestFilters requestFilters)
        : base(ApplyCriteria(requestFilters.SearchValue))
    {
        base.ApplyPaging(requestFilters.PageNumber, requestFilters.PageSize);
        ApplySorting(requestFilters.SortColumn, requestFilters.SortDirction);
    }
    public CategorySpecification()
    {

    }
}
