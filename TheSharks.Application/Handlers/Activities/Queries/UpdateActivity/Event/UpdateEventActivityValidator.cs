using FluentValidation;

namespace TheSharks.Application.Handlers.Activities.Queries.UpdateActivity.Event;

public class UpdateEventActivityValidator : AbstractValidator<UpdateEventActivityQuery>
{
    private const int locationLength = 40;
    private readonly string locationError = $"Locatie mag niet meer dan {locationLength} karakters bevatten";

    private const int locationLinkLength = 512;
    private readonly string locationLinkError = $"Locatielink mag niet meer dan {locationLinkLength} karakters bevatten";

    private const int infoLength = 150;
    private readonly string infoError = $"Info mag niet meer dan {infoLength} karakters bevatten";

    private const string EndError = "Startdatum moet voor einddatum liggen.";
    private const string DepartureError = "Startdatum moet voor einddatum liggen.";

    public UpdateEventActivityValidator()
    {
        RuleFor(x => x.EndTime)
            .GreaterThan(x => x.StartTime.Value)
            .When(x => x.StartTime != null && x.EndTime != null)
            .WithMessage(EndError);

        RuleFor(x => x.Departure)
            .LessThan(x => x.StartTime.Value)
            .When(x => x.StartTime != null && x.Departure != null)
            .WithMessage(DepartureError);

        RuleFor(x => x.Location).MaximumLength(locationLength).WithMessage(locationError);
        RuleFor(x => x.LocationLink).MaximumLength(locationLinkLength).WithMessage(locationLinkError);
        RuleFor(x => x.MemberInfo).MaximumLength(infoLength).WithMessage(infoError);
        RuleFor(x => x.Info).MaximumLength(infoLength).WithMessage(infoError);
    }
}