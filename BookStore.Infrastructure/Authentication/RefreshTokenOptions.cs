using System.ComponentModel.DataAnnotations;

namespace BookStore.Infrastructure.Authentication;

public class RefreshTokenOptions
{
    public static string SectionName { get; } = "RefreshToken";

    [Required, Range(0, int.MaxValue)]
    public int ExpirationInDays { get; init; }
}
