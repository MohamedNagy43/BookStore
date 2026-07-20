using BookStore.Shared.Constants;

namespace BookStore.Application.Common.Contracts;

public record RequestFilters
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = PaginationSettings.DefaultPageSize;
    public string? SearchValue { get; init; }
    public string? SortColumn { get; init; }
    public string? SortDirction { get; init; } = "ASC";
}
