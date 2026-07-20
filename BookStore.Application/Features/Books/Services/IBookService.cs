using BookStore.Application.Common;
using BookStore.Application.Common.Contracts;
using BookStore.Application.Features.Books.Contracts.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStore.Application.Features.Books.Services;

public interface IBookService
{
    Task<PaginatedList<BookResponse>> GetAll(RequestFilters filters, CancellationToken cancellationToken);
}
