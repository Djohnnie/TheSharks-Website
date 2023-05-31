using FluentValidation;

namespace TheSharks.Application.Handlers.Activities.Queries.AddActivity.Dive;

public class AddDiveActivityValidator : AbstractValidator<AddDiveActivityQuery>
{
    private const int locationLength = 40;
    private readonly string locationError = $"Locatie mag niet meer dan {locationLength} karakters bevatten";

    private const int locationLinkLength = 512;
    private readonly string locationLinkError = $"Locatielink mag niet meer dan {locationLinkLength} karakters bevatten";

    private const int infoLength = 150;
    private readonly string infoError = $"Info mag niet meer dan {infoLength} karakters bevatten";

    private readonly string atWaterError = "Te water moet ná de briefing zijn";
    private readonly string departureError = "Briefing moet ná het vertrek zijn";

    public AddDiveActivityValidator()
    {
        RuleFor(x => x.AtWater)
            .GreaterThan(x => x.BriefingTime.Value)
            .When(x => x.AtWater != null && x.BriefingTime != null)
            .WithMessage(atWaterError);

        RuleFor(x => x.BriefingTime)
            .GreaterThan(x => x.Departure.Value)
            .When(x => x.BriefingTime != null && x.Departure != null)
            .WithMessage(departureError);

        RuleFor(x => x.Location).MaximumLength(locationLength).WithMessage(locationError);
        RuleFor(x => x.LocationLink).MaximumLength(locationLinkLength).WithMessage(locationLinkError);
        RuleFor(x => x.MemberInfo).MaximumLength(infoLength).WithMessage(infoError);
        RuleFor(x => x.Info).MaximumLength(infoLength).WithMessage(infoError);
    }
}