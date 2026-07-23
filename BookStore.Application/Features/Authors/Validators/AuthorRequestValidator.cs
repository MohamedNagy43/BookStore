using BookStore.Application.Features.Authors.Contracts.Requests;

namespace BookStore.Application.Features.Authors.Validators;

public class AuthorRequestValidator:AbstractValidator<AuthorRequest>
{
    public AuthorRequestValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.LastName)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.Biography)
            .MaximumLength(1000);

        RuleFor(x=>x.ImageId)
            .NotEmpty();
    }
}
