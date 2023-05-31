using FluentValidation;

namespace TheSharks.Application.Handlers.Members.SendMail;

public class SendMailValidator : AbstractValidator<SendMailQuery>
{
    private readonly string emptyError = "Veldje mag niet leeg zijn";

    //private const int maxLengthSubject = 256;
    //private readonly string subjectlengthError = $"Onderwerp mag niet meer dan {maxLengthSubject} karakters bevatten";

    //private const int maxLengthMessage = 2048;
    //private readonly string messageLengthError = $"Bericht mag niet langer zijn dan {maxLengthMessage} karakters bevatten";

    public SendMailValidator()
    {
        RuleFor(x => x.Message).NotEmpty().WithMessage(emptyError);//.MaximumLength(maxLengthMessage).WithMessage(messageLengthError);
        RuleFor(x => x.Subject).NotEmpty().WithMessage(emptyError);//.MaximumLength(maxLengthSubject).WithMessage(subjectlengthError);
    }
}