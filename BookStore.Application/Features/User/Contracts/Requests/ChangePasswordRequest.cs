namespace BookStore.Application.Features.User.Contracts.Requests;

public record ChangePasswordRequest(string CurrentPassword, string NewPassword);
