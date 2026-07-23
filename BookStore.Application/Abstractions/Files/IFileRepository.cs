using BookStore.Domain.Common;

namespace BookStore.Application.Abstractions.Files;

public interface IFileRepository
{
    Task<StoredFile?> GetFileAsync(Guid id, CancellationToken cancellationToken);
    Task<string?> GetStoreadFileNameAsync(Guid id, CancellationToken cancellationToken);
    Task<bool> ExistAsync(Guid id, CancellationToken cancellationToken);
    Task<bool> ExistAllAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken);
}
