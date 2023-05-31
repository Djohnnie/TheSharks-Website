using FluentValidation;

namespace TheSharks.Application.Handlers.Galleries.Queries.AddGallery;

public class AddGalleryValidator : AbstractValidator<AddGalleryQuery>
{
    private const int maxNameLength = 40;
    private readonly string nameLengthError = $"Naam mag niet meer dan {maxNameLength} karakters bevatten";

    public AddGalleryValidator()
    {
        RuleFor(x => x.Name).MaximumLength(maxNameLength).WithMessage(nameLengthError);
    }
}