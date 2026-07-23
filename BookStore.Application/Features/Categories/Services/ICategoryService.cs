using BookStore.Application.Common;
using BookStore.Application.Common.Contracts;
using BookStore.Application.Features.Authors.Contracts.Requests;
using BookStore.Application.Features.Authors.Contracts.Responses;
using BookStore.Application.Features.Categories.Contracts.Requests;
using BookStore.Application.Features.Categories.Contracts.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStore.Application.Features.Categories.Services;

public interface ICategoryService
{
    Task<PaginatedList<CategoryResponse>> GetAllAsync(RequestFilters filters, CancellationToken cancellationToken = default);
    Task<Result<CategoryResponse>> GetAsync(int id, CancellationToken cancellationToken = default);
    Task<Result<int>> AddAsync(CategoryRequest request, CancellationToken cancellationToken = default);
    Task<Result> UpdateAsync(int id, CategoryRequest request, CancellationToken cancellationToken = default);
    Task<Result> DeleteAsync(int id, CancellationToken cancellationToken = default);
    Task<Result> RestoreAsync(int id, CancellationToken cancellationToken = default);
}
