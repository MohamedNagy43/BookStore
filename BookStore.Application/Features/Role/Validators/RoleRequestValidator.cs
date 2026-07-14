using BookStore.Application.Features.Role.Contracts.Requests;

namespace BookStore.Application.Features.Role.Validators;

public class RoleRequestValidator : AbstractValidator<RoleRequest>
{
    public RoleRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .Length(1, 200);

        RuleFor(x => x.Permissions)
            .NotEmpty();


        RuleFor(x => x.Permissions)
            .Must(x => x.Count == x.Distinct().Count()).When(x => x.Permissions is not null)
            .WithMessage("you can not add dublicated values");

    }
}
