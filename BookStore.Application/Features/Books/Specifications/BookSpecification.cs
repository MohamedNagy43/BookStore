using BookStore.Application.Common.Contracts;
using BookStore.Domain.Entities.Books;
using System.Linq.Expressions;

namespace BookStore.Application.Features.Books.Specifications;

public class BookSpecification : Specification<Book>, ISpecification<Book>
{
    private static Expression<Func<Book, bool>>? ApplyCriteria(string? searchValue)
    {
        if (string.IsNullOrEmpty(searchValue))
            return null;

        return x => x.Title.Contains(searchValue);
    }
    private void ApplyIncludes()
    {
        base.AddIncludes(x => x.BookFiles, x => x.Category, x => x.Author);
    }
    private void ApplySorting(string? sortColumn, string sortDirction = "ASC")
    {
        if (string.IsNullOrWhiteSpace(sortColumn))
            return;

        Expression<Func<Book, object>> expression = sortColumn.ToLower() switch
        {
            "title" => x => x.Title,
            "price" => x => x.Price,
            "weight" => x => x.Weight,
            _ => x => x.Title
        };

        if (sortDirction.Equals("DESC", StringComparison.OrdinalIgnoreCase))
            ApplyOrderByDescending(expression);
        else
            ApplyOrderBy(expression);
    }
    public BookSpecification(RequestFilters requestFilters)
        : base(ApplyCriteria(requestFilters.SearchValue))
    {
        ApplyIncludes();
        base.ApplyPaging(requestFilters.PageNumber, requestFilters.PageSize);
        ApplySorting(requestFilters.SortColumn,requestFilters.SortDirction!);

    }
}