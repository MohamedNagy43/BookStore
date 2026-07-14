namespace BookStore.Application.Features.Authentication.Validators;

public class ResendEmailConfirmationCodeRequestValidator : AbstractValidator<ResendEmailConfirmationCodeRequest>
{
    public ResendEmailConfirmationCodeRequestValidator()
    {

        RuleFor(r => r.Email)
            .NotEmpty()
            .EmailAddress();
    }
}
