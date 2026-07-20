using BookStore.Domain.Entities.Common;

namespace BookStore.Application.Abstractions.Files;

public interface IFileRepository
{
    Task<StoredFile?> GetFile(Guid id, CancellationToken cancellationToken);
}
