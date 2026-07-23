using System;
using System.Collections.Generic;
using System.Text;

namespace BookStore.Application.Features.Categories.Errors;

public static class CategoryErrors
{
    public static Error DuplicatedName => new("Category.DuplicatedName",
       "There is entity with the same name", ErrorType.Validation);
}
