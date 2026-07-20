using BookStore.Application.Abstractions.Repositories;
using BookStore.Application.Common;
using BookStore.Application.Common.Contracts;
using BookStore.Application.Features.Books.Contracts.Responses;
using BookStore.Application.Features.Books.Specifications;
using BookStore.Domain.Entities.Book;
using Mapster;

namespace BookStore.Application.Features.Books.Services;

public class BookService(IUnitOfWork unitOfWork) : IBookService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<PaginatedList<BookResponse>> GetAll(RequestFilters filters, CancellationToken cancellationToken)
    {
        var specification = new BookSpecification(filters);
        var bookRepo = _unitOfWork.Repository<Book, Guid>();
        var books = (await bookRepo.GetAllAsync(specification, cancellationToken)).Adapt<IEnumerable<BookResponse>>();
        var count = await bookRepo.GetCountAsync(cancellationToken: cancellationToken);
        var paginatedList = new PaginatedList<BookResponse>(books.ToList(), filters.PageNumber, count, filters.PageSize);

        return paginatedList;

    }
}
