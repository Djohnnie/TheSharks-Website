using FluentValidation;

namespace TheSharks.Application.Handlers.Identity.Queries.Authentication.ForgotPassword;

public class ForgotPasswordValidator : AbstractValidator<ForgotPasswordQuery>
{
    private const int maxLength = 45;
    private readonly string emptyError = "Email mag niet leeg zijn";
    private readonly string lengthError = $"Email mag niet meer dan {maxLength} karakters bevatten";
    private readonly string invalidError = "Geef een geldig email in";

    public ForgotPasswordValidator()
    {
        RuleFor(x => x.Email).NotEmpty().WithMessage(emptyError)
            .EmailAddress().WithMessage(invalidError)
            .MaximumLength(maxLength).WithMessage(lengthError);
    }
}