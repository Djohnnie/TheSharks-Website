using FluentValidation;
using TheSharks.Contracts.Models.Enrollments;

namespace TheSharks.Application.Handlers.Enrollments.Queries.AddEnrollment.EnrollmentDelegator;

public class EnrollmentDelegatorValidator : AbstractValidator<EnrollmentDelegatorQuery>
{
    public EnrollmentDelegatorValidator()
    {
        RuleForEach(x => x.GuestEnrollments).SetValidator(new EnrollmentGuestValidator());
    }
}

public class EnrollmentGuestValidator : AbstractValidator<EnrollmentGuestModel>
{
    private const int minNameLength = 3;
    private const int maxNameLength = 60;
    private readonly string nameLengthError = $"Naam moet tussen de {minNameLength} en {maxNameLength} karakters bevatten";
    private readonly string diveCretificateRequiredError = "Een duikbrevet is verplicht voor een gast die is ingeschreven als duiker";

    private const int maxCertificateLength = 20;
    private readonly string certificateLengthError = $"Certificaat mag niet meer dan {maxCertificateLength} karakters bevatten";

    public EnrollmentGuestValidator()
    {
        RuleFor(x => x.Registree).Length(minNameLength, maxNameLength).WithMessage(nameLengthError);
        RuleFor(x => x.DiveCertificate).NotEmpty().When(x => x.AsDiver).WithMessage(diveCretificateRequiredError);
        RuleFor(x => x.DiveCertificate).MaximumLength(maxCertificateLength).WithMessage(certificateLengthError);
    }
}