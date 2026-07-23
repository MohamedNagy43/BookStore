using BookStore.Application.Abstractions.Files;
using BookStore.Domain.Common;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStore.Infrastructure.Persistence.Repositories;

internal class FileRepository(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment) : IFileRepository
{
    private readonly ApplicationDbContext _context = context;
    private readonly string _filePath = webHostEnvironment.WebRootPath;

    public async Task<bool> ExistAllAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken)
    {
        var idsList = ids.ToList();

        var existingIds = await _context.StoredFiles
            .Where(x => idsList.Contains(x.Id))
            .Select(x => x.Id)
            .ToListAsync(cancellationToken);

        return !idsList.Except(existingIds).Any();
    }

    public async Task<bool> ExistAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.StoredFiles.AnyAsync(x => x.Id == id, cancellationToken);
    }
    public async Task<StoredFile?> GetFileAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.StoredFiles.FindAsync(id, cancellationToken);
    }
    public async Task<string?> GetStoreadFileNameAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.StoredFiles
            .Where(x => x.Id == id)
            .Select(x => x.StoredFileName)
            .SingleOrDefaultAsync(cancellationToken);
    }
}
