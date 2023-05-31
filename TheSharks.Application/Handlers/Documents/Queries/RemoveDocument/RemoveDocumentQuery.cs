using MediatR;
using TheSharks.Contracts.Models.Common;

namespace TheSharks.Application.Handlers.Documents.Queries.RemoveDocument;

public class RemoveDocumentQuery : BaseIdModel, IRequest<BaseIdModel>
{
    public string FileName { get; set; }
}