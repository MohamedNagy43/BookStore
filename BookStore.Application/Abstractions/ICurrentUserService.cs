namespace BookStore.Application.Abstractions;

public interface ICurrentUserService
{
    string? UserId { get; }
}
