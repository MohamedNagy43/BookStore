using BookStore.Shared.Constants;
using FluentValidation;

namespace BookStore.Application.Common.Contracts;

public class RequestFiltersValidator : AbstractValidator<RequestFilters>
{
    public RequestFiltersValidator()
    {
        RuleFor(x => x.PageSize)
            .GreaterThanOrEqualTo(1)
            .LessThanOrEqualTo(PaginationSettings.MaxPageSize);

        RuleFor(x => x.PageNumber)
            .GreaterThanOrEqualTo(1);


        RuleFor(x => x.SortDirction)
            .Matches(RegexPatterns.SortDirction)
            .WithMessage("Format accept only ASC and DESC");

    }
}
