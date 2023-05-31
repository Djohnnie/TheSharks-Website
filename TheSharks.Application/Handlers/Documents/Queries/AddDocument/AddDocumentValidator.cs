using FluentValidation;

namespace TheSharks.Application.Handlers.Documents.Queries.AddDocument;

public class AddDocumentValidator : AbstractValidator<AddDocumentQuery>
{
    private const int nameLength = 50;
    private readonly string nameError = $"Naam mag niet meer dan {nameLength} karakters bevatten";

    public AddDocumentValidator()
    {
        RuleFor(x => x.Name).MaximumLength(nameLength).WithMessage(nameError);
    }
}