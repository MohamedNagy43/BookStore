using BookStore.Application.Abstractions.Common;
using BookStore.Application.Abstractions.Files;
using BookStore.Application.Abstractions.Files.Contracts;
using BookStore.Domain.Common;
using BookStore.Shared.Constants;
using Microsoft.AspNetCore.Hosting;

namespace BookStore.Infrastructure.Services;

public class FileService(IWebHostEnvironment webHostEnvironment, ApplicationDbContext context, ICurrentUserService currentUserService) : IFileService
{
    private readonly string _filePath = $"{webHostEnvironment.WebRootPath}/{FileSettings.FileUploadPath}";
    private readonly ApplicationDbContext _context = context;
    private readonly ICurrentUserService _currentUserService = currentUserService;

    public async Task<Guid> UploadAsync(FileRequest request, CancellationToken cancellationToken = default)
    {

        var storedFile = await SaveFile(request, cancellationToken);

        await _context.StoredFiles.AddAsync(storedFile, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return storedFile.Id;
    }
    public async Task<IEnumerable<Guid>> UploadManyAsync(MultipleFileRequest request, CancellationToken cancellationToken = default)
    {
        List<StoredFile> files = [];
        foreach (var fileRequest in request.Files)
        {
            var storedFile = await SaveFile(fileRequest, cancellationToken);
            files.Add(storedFile);
        }
        await _context.StoredFiles.AddRangeAsync(files, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return files.Select(x => x.Id);
    }
    public async Task<(byte[] fileContent, string fileName, string contentType)> DownloadAsync(Guid id, CancellationToken cancellationToken = default)
    {
        if (await _context.StoredFiles.FindAsync(id, cancellationToken) is not { } file)
            return ([], string.Empty, string.Empty);

        using var fileStream = File.OpenRead(Path.Combine(_filePath, file.StoredFileName));
        MemoryStream memeorystream = new();
        await fileStream.CopyToAsync(memeorystream, cancellationToken);
        memeorystream.Position = 0;
        return (memeorystream.ToArray(), file.FileName, file.ContentType);
    }
    public async Task<(FileStream? stream, string fileName, string contentType)> StreamAsync(Guid id, CancellationToken cancellationToken = default)
    {
        if (await _context.StoredFiles.FindAsync(id, cancellationToken) is not { } file)
            return (null, "", "");

        var stream = File.OpenRead(Path.Combine(_filePath, file.StoredFileName));
        return (stream, file.FileName, file.ContentType);
    }


    // Private
    private async Task<StoredFile> SaveFile(FileRequest request, CancellationToken cancellationToken = default)
    {
        var extension = Path.GetExtension(request.FileName);
        var storeadFileName = $"{Guid.CreateVersion7()}.{extension}";


        request.FileStream.Position = 0;
        var storedFile = new StoredFile
        {
            FileName = request.FileName,
            StoredFileName = storeadFileName,
            ContentType = request.ContentType,
            FileExtension = extension,
            Size = request.FileStream.Length,
            Path = FileSettings.FileUploadPath,
            UploadedById = _currentUserService.UserId!
        };

        string path = Path.Combine(_filePath, storeadFileName);

        using var stream = File.Create(path);
        await request.FileStream.CopyToAsync(stream, cancellationToken);

        return storedFile;
    }
}
