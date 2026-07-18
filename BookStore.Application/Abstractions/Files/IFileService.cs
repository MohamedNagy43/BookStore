namespace BookStore.Application.Abstractions.Files;

public interface IFileService
{
    Task<Guid> UploadAsync(FileRequest request, CancellationToken cancellationToken = default);
    Task<IEnumerable<Guid>> UploadManyAsync(List<FileRequest> fileStreams, CancellationToken cancellationToken = default);
    Task<(byte[] fileContent, string fileName, string contentType)> DownloadAsync(Guid id, CancellationToken cancellationToken = default);
    Task<(FileStream? stream, string fileName, string contentType)> StreamAsync(Guid id, CancellationToken cancellationToken = default);
}
