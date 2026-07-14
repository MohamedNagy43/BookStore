namespace BookStore.Application.Features.Role.Services;

public interface IRoleService
{
    Task<IEnumerable<RoleResponse>> GetAllAsync(bool includeDisabled = false, CancellationToken cancellationToken = default);
    Task<Result<RoleDetailsResponse>> GetAsync(string id);
    Task<Result<RoleDetailsResponse>> AddAsync(RoleRequest request, CancellationToken cancellationToken = default);
    Task<Result> UpdateAsync(string id, RoleRequest request, CancellationToken cancellationToken = default);
    Task<Result> ToggleStatusAsync(string id, CancellationToken cancellationToken = default);
}
