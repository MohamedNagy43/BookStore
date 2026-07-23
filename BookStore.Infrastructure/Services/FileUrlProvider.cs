using BookStore.Application.Abstractions.Files;

namespace BookStore.Infrastructure.Services;

public class FileUrlProvider(IHttpContextAccessor httpContextAccessor) : IFileUrlProvider
{
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    public string GetFileFullUrl(string relativePath)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(relativePath);

        var request = _httpContextAccessor.HttpContext?.Request
            ?? throw new InvalidOperationException("No active HTTP request.");

        return $"{request.Scheme}://{request.Host}/{relativePath}";
    }
}
