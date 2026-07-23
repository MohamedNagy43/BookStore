using BookStore.Application.Abstractions.Repositories;
using BookStore.Application.Common;
using BookStore.Application.Common.Contracts;
using BookStore.Application.Features.Authors.Contracts.Responses;
using BookStore.Application.Features.Categories.Contracts.Requests;
using BookStore.Application.Features.Categories.Contracts.Responses;
using BookStore.Application.Features.Categories.Errors;
using BookStore.Application.Features.Categories.Specification;
using BookStore.Domain.Entities.Authors;
using BookStore.Domain.Entities.Categories;
using Mapster;

namespace BookStore.Application.Features.Categories.Services;

public class CategoryService(IUnitOfWork unitOfWork) : ICategoryService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<PaginatedList<CategoryResponse>> GetAllAsync(RequestFilters filters, CancellationToken cancellationToken = default)
    {
        var specification = new CategorySpecification(filters);
        var categoryRepo = _unitOfWork.Repository<Category, int>();

        var count = await categoryRepo.GetCountAsync(cancellationToken: cancellationToken);
        var response = (await categoryRepo.GetAllAsync(specification, cancellationToken))
            .Adapt<IEnumerable<CategoryResponse>>();

        return new PaginatedList<CategoryResponse>(response, filters.PageNumber, count, filters.PageSize);
    }
    public async Task<Result<CategoryResponse>> GetAsync(int id, CancellationToken cancellationToken = default)
    {
        var categoryRepo = _unitOfWork.Repository<Category, int>();

        if (await categoryRepo.GetByIdAsync(id, cancellationToken: cancellationToken) is not { } category)
            return Result.Failure<CategoryResponse>(Error.NotFoundEntity<Category>());

        var response = category.Adapt<CategoryResponse>();

        return Result.Success(response);
    }
    public async Task<Result<int>> AddAsync(CategoryRequest request, CancellationToken cancellationToken = default)
    {
        var categoryRepo = _unitOfWork.Repository<Category, int>();

        var isExistName = await categoryRepo.ExistAsync(x => x.Name == request.Name, cancellationToken);
        if (isExistName)
            return Result.Failure<int>(CategoryErrors.DuplicatedName);

        var category = request.Adapt<Category>();

        await categoryRepo.AddAsync(category, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(category.Id);
    }
    public async Task<Result> UpdateAsync(int id, CategoryRequest request, CancellationToken cancellationToken = default)
    {
        var categoryRepo = _unitOfWork.Repository<Category, int>();

        if (await categoryRepo.GetByIdAsync(id, cancellationToken: cancellationToken) is not { } category)
            return Result.Failure<CategoryResponse>(Error.NotFoundEntity<Category>());


        var isExistName = await categoryRepo.ExistAsync(x => x.Name == request.Name && x.Id != id, cancellationToken);
        if (isExistName)
            return Result.Failure<int>(CategoryErrors.DuplicatedName);

        category = request.Adapt(category);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
    public async Task<Result> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {

        if (await _unitOfWork.Repository<Category, int>().GetByIdAsync(id, includeDeleted: true, cancellationToken: cancellationToken) is not { } category)
            return Result.Failure(Error.NotFoundEntity<Category>());

        if (category.IsDeleted)
            return Result.Failure(Error.DeletedEntity<Category>());

        category.IsDeleted = true;
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
    public async Task<Result> RestoreAsync(int id, CancellationToken cancellationToken = default)
    {
        if (await _unitOfWork.Repository<Category, int>().GetByIdAsync(id, includeDeleted: true, cancellationToken: cancellationToken) is not { } category)
            return Result.Failure(Error.NotFoundEntity<Category>());

        if (!category.IsDeleted)
            return Result.Failure(Error.ActiveEntity<Category>());

        category.IsDeleted = false;
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
