namespace BookStore.Domain.Entities.Common;

public class StoredFile
{
    public Guid Id { get; set; } = Guid.CreateVersion7();
    public string FileName { get; set; } = string.Empty;
    public string StoredFileName { get; set; } = string.Empty;
    public string ContentType { get; set; } = string.Empty;
    public string FileExtension { get; set; } = string.Empty;
    public string Path { get; set; } = string.Empty;
    public long Size { get; set; }
    public string UploadedById { get; set; } = string.Empty;
    public DateTime UploadedOn { get; set; } = DateTime.UtcNow;
}
