namespace BookStore.Application.Features.Authentication.Validators;

public class ForgetPasswordRequestValidator : AbstractValidator<ForgetPasswordRequest>
{

    public ForgetPasswordRequestValidator()
    {
        RuleFor(r => r.Email)
            .NotEmpty()
            .EmailAddress();
    }
}
