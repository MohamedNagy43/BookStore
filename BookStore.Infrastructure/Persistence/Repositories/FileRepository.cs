using BookStore.Application.Abstractions.Files;
using BookStore.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStore.Infrastructure.Persistence.Repositories;

internal class FileRepository(ApplicationDbContext context) : IFileRepository
{
    private readonly ApplicationDbContext _context = context;

    public async Task<StoredFile?> GetFile(Guid id, CancellationToken cancellationToken)
    {
        return await _context.StoredFiles.FindAsync(id,cancellationToken);
    }
}
