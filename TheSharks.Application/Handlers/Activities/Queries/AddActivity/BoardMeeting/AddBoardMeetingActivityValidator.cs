﻿using FluentValidation;

namespace TheSharks.Application.Handlers.Activities.Queries.AddActivity.BoardMeeting;

public class AddBoardMeetingActivityValidator : AbstractValidator<AddBoardMeetingActivityQuery>
{
    private const int locationLength = 40;
    private readonly string locationError = $"Locatie mag niet meer dan {locationLength} karakters bevatten";

    private const int locationLinkLength = 512;
    private readonly string locationLinkError = $"Locatielink mag niet meer dan {locationLinkLength} karakters bevatten";

    private const int infoLength = 150;
    private readonly string infoError = $"Info mag niet meer dan {infoLength} karakters bevatten";

    private const string DateError = "Startdatum moet voor einddatum liggen.";

    public AddBoardMeetingActivityValidator()
    {
        RuleFor(x => x.EndTime)
            .GreaterThan(x => x.StartTime.Value)
            .When(x => x.StartTime != null && x.EndTime != null)
            .WithMessage(DateError);

        RuleFor(x => x.Location).MaximumLength(locationLength).WithMessage(locationError);
        RuleFor(x => x.LocationLink).MaximumLength(locationLinkLength).WithMessage(locationLinkError);
        RuleFor(x => x.MemberInfo).MaximumLength(infoLength).WithMessage(infoError);
        RuleFor(x => x.Info).MaximumLength(infoLength).WithMessage(infoError);
    }
}