using BookStore.Domain.Entities.Common;

namespace BookStore.Application.Abstractions.Repositories;

public interface IFileRepository
{
    Task<StoredFile?> GetFile(Guid id, CancellationToken cancellationToken);
}
