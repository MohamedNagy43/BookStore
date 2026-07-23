using System;
using System.Collections.Generic;
using System.Text;

namespace BookStore.Application.Features.Books.Errors;

public static class BookErrors
{
    public static Error AuthorNotFound => new Error("Book.AuthorNotFound"
   , "No Author found with this key", ErrorType.Validation);

    public static Error CategoryNotFound => new Error("Book.CategoryNotFound"
   , "No Category found with this key", ErrorType.Validation);

    public static Error FilesNotFound => new Error("Book.FilesNotFound"
   , "Some of theses files ids are not found", ErrorType.Validation);

    public static Error DuplicatedTitle => new Error("Book.DuplicatedTitle"
   , "this title is alreaady exist", ErrorType.Validation);
}
