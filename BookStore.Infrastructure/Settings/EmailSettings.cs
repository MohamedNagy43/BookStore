using System.ComponentModel.DataAnnotations;

namespace BookStore.Infrastructure.Settings;

public class EmailSettings
{
    public static readonly string SectionName = "EmailSettings";

    [Required]
    public string Host { get; init; } = string.Empty;
    [Required,Range(100,999)]
    public int Port { get; init; }
    [Required]
    public string DisplayName { get; init; } = string.Empty;
    [Required,EmailAddress]
    public string Email { get; init; } = string.Empty;
    [Required]
    public string Password { get; init; } = string.Empty;
}
