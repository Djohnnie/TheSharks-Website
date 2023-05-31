using FluentValidation;

namespace TheSharks.Application.Handlers.NewsItems.Queries.UpdateNewsItem;

public class UpdateNewsItemValidator : AbstractValidator<UpdateNewsItemQuery>
{
    private const int maxTitleLength = 60;
    private readonly string titleError = $"Titel mag niet meer dan {maxTitleLength} karakters bevatten";

    public UpdateNewsItemValidator()
    {
        RuleFor(x => x.Title).MaximumLength(maxTitleLength).WithMessage(titleError);
    }
}