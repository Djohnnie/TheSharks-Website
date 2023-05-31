using FluentValidation;

namespace TheSharks.Application.Handlers.OpenWaterTests.Commands.AddOpenWaterTest;

internal class AddOpenWaterTestCommandValidator : AbstractValidator<AddOpenWaterTestCommand>
{
    private const int titleLength = 120;
    private readonly string titleError = $"Titel mag niet meer dan {titleLength} karakters bevatten";

    private const int diveCertificateLength = 30;
    private readonly string diveCertificateError = $"Kandidaat mag niet meer dan {diveCertificateLength} karakters bevatten";

    public AddOpenWaterTestCommandValidator()
    {
        RuleFor(x => x.Title).MaximumLength(titleLength).WithMessage(titleError);
        RuleFor(x => x.DiveCertificate).MaximumLength(diveCertificateLength).WithMessage(diveCertificateError);
    }
}