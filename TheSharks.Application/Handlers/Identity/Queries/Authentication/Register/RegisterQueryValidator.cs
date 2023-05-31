using FluentValidation;

namespace TheSharks.Application.Handlers.Identity.Queries.Authentication.Register;

public class RegisterQueryValidator : AbstractValidator<RegisterQuery>
{
    private const int maxLength = 45;
    private readonly string emptyError = "Email mag niet leeg zijn";
    private readonly string lengthError = $"Email mag niet meer dan {maxLength} karakters bevatten";
    private readonly string invalidError = "Geef een geldig email in";

    private const int maxLengthName = 30;
    private readonly string usernameError = $"Gebruikersnaam mag niet langer zijn dan ${maxLengthName} karakters bevatten";
    private readonly string firstNameError = $"Voornaam mag niet langer zijn dan ${maxLengthName} karakters bevatten";
    private readonly string lastNameError = $"Achternaam mag niet langer zijn dan ${maxLengthName} karakters bevatten";

    public RegisterQueryValidator()
    {
        RuleFor(x => x.UserName).NotEmpty().WithMessage(emptyError).MaximumLength(maxLengthName).WithMessage(usernameError);
        RuleFor(x => x.FirstName).NotEmpty().WithMessage(emptyError).MaximumLength(maxLengthName).WithMessage(firstNameError);
        RuleFor(x => x.LastName).NotEmpty().WithMessage(emptyError).MaximumLength(maxLengthName).WithMessage(lastNameError);

        RuleFor(x => x.Email).NotEmpty().WithMessage(emptyError)
             .EmailAddress().WithMessage(invalidError)
             .MaximumLength(maxLength).WithMessage(lengthError);
    }
}