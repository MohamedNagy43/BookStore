using BookStore.Application.Abstractions.Files;
using BookStore.Application.Abstractions.Repositories;
using BookStore.Application.Common;
using BookStore.Application.Common.Contracts;
using BookStore.Application.Features.Authors.Contracts.Requests;
using BookStore.Application.Features.Authors.Contracts.Responses;
using BookStore.Application.Features.Authors.Specification;
using BookStore.Domain.Common;
using BookStore.Domain.Entities.Authors;
using BookStore.Shared.Constants;
using Mapster;

namespace BookStore.Application.Features.Authors.Services;

public class AutherService(IUnitOfWork unitOfWork, IFileRepository fileRepository, IFileUrlProvider fileUrlProvider) : IAutherService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IFileRepository _fileRepository = fileRepository;
    private readonly IFileUrlProvider _fileUrlProvider = fileUrlProvider;

    public async Task<PaginatedList<AuthorResponse>> GetAllAsync(RequestFilters filters, CancellationToken cancellationToken = default)
    {
        var specification = new AuthorSpecification(filters);
        var authorRepo = _unitOfWork.Repository<Author, int>();

        var authors = await authorRepo.GetAllAsync(specification, cancellationToken);

        var authorResponse = authors.Select(x => new AuthorResponse(x.Id, x.FirstName, x.LastName, x.Biography!,
             _fileUrlProvider.GetFileFullUrl($"{FileSettings.FileUploadPath}/{x.File.StoredFileName}")));

        var count = await authorRepo.GetCountAsync(cancellationToken: cancellationToken);


        return new PaginatedList<AuthorResponse>(authorResponse, filters.PageNumber, count, filters.PageSize);
    }
    public async Task<Result<AuthorResponse>> GetAsync(int id, CancellationToken cancellationToken = default)
    {
        var specification = new AuthorSpecification();
        if (await _unitOfWork.Repository<Author, int>().GetByIdAsync(id, specification, cancellationToken: cancellationToken) is not { } author)
            return Result.Failure<AuthorResponse>(Error.NotFoundEntity<Author>());

        var response = author.Adapt<AuthorResponse>()
            with
        { ImageUrl = _fileUrlProvider.GetFileFullUrl($"{FileSettings.FileUploadPath}/{author.File.StoredFileName}") };

        return Result.Success(response);
    }
    public async Task<Result<int>> AddAsync(AuthorRequest request, CancellationToken cancellationToken = default)
    {
        // Validations
        if (!await _fileRepository.ExistAsync(request.ImageId, cancellationToken))
            return Result.Failure<int>(Error.NotFoundEntity<StoredFile>());

        // Add

        var author = request.Adapt<Author>();
        await _unitOfWork.Repository<Author, int>().AddAsync(author, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(author.Id);
    }
    public async Task<Result> UpdateAsync(int id, AuthorRequest request, CancellationToken cancellationToken = default)
    {

        if (await _unitOfWork.Repository<Author, int>().GetByIdAsync(id, cancellationToken: cancellationToken) is not { } author)
            return Result.Failure(Error.NotFoundEntity<Author>());

        request.Adapt(author);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
    public async Task<Result> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        if (await _unitOfWork.Repository<Author, int>().GetByIdAsync(id, includeDeleted: true, cancellationToken: cancellationToken) is not { } author)
            return Result.Failure(Error.NotFoundEntity<Author>());

        if (author.IsDeleted)
            return Result.Failure(Error.DeletedEntity<Author>());

        author.IsDeleted = true;
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
    public async Task<Result> RestoreAsync(int id, CancellationToken cancellationToken = default)
    {
        if (await _unitOfWork.Repository<Author, int>().GetByIdAsync(id, includeDeleted: true, cancellationToken: cancellationToken) is not { } author)
            return Result.Failure(Error.NotFoundEntity<Author>());

        if (!author.IsDeleted)
            return Result.Failure(Error.ActiveEntity<Author>());

        author.IsDeleted = false;
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
