namespace BookStore.Application.Abstractions.Common;

public interface ICurrentUserService
{
    string? UserId { get; }
}
