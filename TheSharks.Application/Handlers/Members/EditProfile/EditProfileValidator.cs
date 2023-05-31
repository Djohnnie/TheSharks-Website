using FluentValidation;

namespace TheSharks.Application.Handlers.Members.EditProfile;

public class EditProfileValidator : AbstractValidator<EditProfileQuery>
{
    private const int maxLengthBio = 80;
    private readonly string bioLengthError = $"Bio mag niet meer dan {maxLengthBio} karakters bevatten";

    private const int maxLengthPhone = 13;
    private readonly string phoneLengthError = $"Telefoonnummer mag niet meer dan {maxLengthPhone} karakters bevatten";

    public EditProfileValidator()
    {
        RuleFor(x => x.PhoneNumber).MaximumLength(maxLengthPhone).WithMessage(phoneLengthError);
        RuleFor(x => x.Bio).MaximumLength(maxLengthBio).WithMessage(bioLengthError);
    }
}