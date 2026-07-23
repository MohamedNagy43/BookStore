using BookStore.Application.Common;
using BookStore.Application.Common.Contracts;
using BookStore.Application.Features.Authors.Contracts.Requests;
using BookStore.Application.Features.Authors.Contracts.Responses;

namespace BookStore.Application.Features.Authors.Services;

public interface IAutherService
{
    Task<PaginatedList<AuthorResponse>> GetAllAsync(RequestFilters filters, CancellationToken cancellationToken = default);
    Task<Result<AuthorResponse>> GetAsync(int id, CancellationToken cancellationToken = default);
    Task<Result<int>> AddAsync(AuthorRequest request, CancellationToken cancellationToken = default);
    Task<Result> UpdateAsync(int id, AuthorRequest request, CancellationToken cancellationToken = default);
    Task<Result> DeleteAsync(int id, CancellationToken cancellationToken = default);
    Task<Result> RestoreAsync(int id, CancellationToken cancellationToken = default);
}
