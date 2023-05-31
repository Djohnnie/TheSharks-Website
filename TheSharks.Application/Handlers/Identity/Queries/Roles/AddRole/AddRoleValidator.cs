using FluentValidation;

namespace TheSharks.Application.Handlers.Identity.Queries.Roles.AddRole;

public class AddRoleValidator : AbstractValidator<AddRoleQuery>
{
    private const int maxLength = 256;
    private readonly string emptyError = "Naam mag niet leeg zijn";
    private readonly string lengthError = $"Naam mag niet meer dan {maxLength} karakters bevatten";

    public AddRoleValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage(emptyError).MaximumLength(maxLength).WithMessage(lengthError);
    }
}