using BookStore.Application.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStore.Application.Features.Books.Services;

public interface IBookService
{
    Task<PaginatedList<BookService>> GetAll();
}
