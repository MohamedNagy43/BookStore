using BookStore.Application.Abstractions.Repositories;
using BookStore.Application.Common;
using BookStore.Domain.Entities.Book;

namespace BookStore.Application.Features.Books.Services;

public class BookService(IUnitOfWork unitOfWork) : IBookService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public Task<PaginatedList<BookService>> GetAll()
    {
        var response = _unitOfWork.Repository<Book,Guid>().GetAllAsync();
    }
}
