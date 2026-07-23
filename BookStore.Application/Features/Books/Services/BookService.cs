using BookStore.Application.Abstractions.Files;
using BookStore.Application.Abstractions.Repositories;
using BookStore.Application.Common;
using BookStore.Application.Common.Contracts;
using BookStore.Application.Features.Books.Contracts.Requests;
using BookStore.Application.Features.Books.Contracts.Responses;
using BookStore.Application.Features.Books.Errors;
using BookStore.Application.Features.Books.Specifications;
using BookStore.Domain.Entities.Authors;
using BookStore.Domain.Entities.Books;
using BookStore.Domain.Entities.Categories;
using BookStore.Shared.Constants;
using Mapster;

namespace BookStore.Application.Features.Books.Services;

public class BookService(IUnitOfWork unitOfWork, IFileRepository fileRepository,IFileUrlProvider fileUrlProvider) : IBookService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IFileRepository _fileRepository = fileRepository;
    private readonly IFileUrlProvider _fileUrlProvider = fileUrlProvider;

    public async Task<PaginatedList<BookResponse>> GetAll(RequestFilters filters, CancellationToken cancellationToken)
    {
        //var specification = new BookSpecification(filters);
        //var bookRepo = _unitOfWork.Repository<Book, Guid>();

        //var books = (await bookRepo.GetAllAsync(specification, cancellationToken));
        //var count = await bookRepo.GetCountAsync(cancellationToken: cancellationToken);

        //var bookResponse = await Task.WhenAll(books.Select(async x => new BookResponse(
        //    x.Id, x.Title, x.Description, x.Price, x.PageCount, x.Language, x.PublisherName, x.PublicationDate, x.Edition, x.Weight, x.IsAvailable,
        //    _fileUrlProvider.GetFileFullUrl($"{FileSettings.FileUploadPath}/{await _fileRepository.GetStoreadFileNameAsync()}")
        //    ));

        //var paginatedList = new PaginatedList<BookResponse>(bookResponse, filters.PageNumber, count, filters.PageSize);

        //return paginatedList;

        throw new NotImplementedException();
    }
    public async Task<Result> Add(CreateBookRequest request, CancellationToken cancellationToken)
    {
        // validation
        var bookRepo = _unitOfWork.Repository<Book, Guid>();

        if (await _unitOfWork.Repository<Author, int>().ExistAsync(request.AuthorId, cancellationToken))
            return Result.Failure(BookErrors.AuthorNotFound);

        if (await _unitOfWork.Repository<Category, int>().ExistAsync(request.CategoryId, cancellationToken))
            return Result.Failure(BookErrors.CategoryNotFound);

        if (!await _fileRepository.ExistAsync(request.CoverImageId, cancellationToken) || !await _fileRepository.ExistAllAsync(request.GalleryImageIds, cancellationToken))
            return Result.Failure(BookErrors.FilesNotFound);

        if (await bookRepo.ExistAsync(x => x.Title == request.Title))
            return Result.Failure(BookErrors.DuplicatedTitle);

        // Add
        var book = request.Adapt<Book>();
        book.BookFiles.Add(new BookFile
        {
            FileId = request.CoverImageId,
            IsPrimary = true,
            DisplayOrder = 1,
        });

        int i = 2;
        request.GalleryImageIds.ToList().ForEach(x => book.BookFiles.Add(new BookFile
        {
            FileId = x,
            DisplayOrder = i++,
        }));

        await bookRepo.AddAsync(book, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
