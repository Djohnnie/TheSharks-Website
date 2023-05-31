using FluentValidation;

namespace TheSharks.Application.Handlers.NewsItems.Queries.AddNewsItem;

public class AddNewsItemValidator : AbstractValidator<AddNewsItemQuery>
{
    private const int maxTitleLength = 60;
    private readonly string titleError = $"Titel mag niet meer dan {maxTitleLength} karakters bevatten";

    public AddNewsItemValidator()
    {
        RuleFor(x => x.Title).MaximumLength(maxTitleLength).WithMessage(titleError);
    }
}