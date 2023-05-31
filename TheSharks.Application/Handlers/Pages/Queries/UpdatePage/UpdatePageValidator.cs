using FluentValidation;

namespace TheSharks.Application.Handlers.Pages.Queries.AddPage;

public class UpdatePageValidator : AbstractValidator<AddPageQuery>
{
    private const int maxTitleLength = 30;
    private readonly string titleError = $"Titel mag niet meer dan {maxTitleLength} karakters bevatten";

    private const int maxLinkLength = 120;
    private readonly string linkError = $"Bericht mag niet meer dan {maxLinkLength} karakters bevatten";

    public UpdatePageValidator()
    {
        RuleFor(x => x.Title).MaximumLength(maxTitleLength).WithMessage(titleError);
        RuleFor(x => x.Link).MaximumLength(maxLinkLength).WithMessage(linkError);
    }
}