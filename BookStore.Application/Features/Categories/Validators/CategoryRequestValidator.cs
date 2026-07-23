using BookStore.Application.Features.Categories.Contracts.Requests;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStore.Application.Features.Categories.Validators;

internal class CategoryRequestValidator : AbstractValidator<CategoryRequest>
{
    public CategoryRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty();
    }
}
