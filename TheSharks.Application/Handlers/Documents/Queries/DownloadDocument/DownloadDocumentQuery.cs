using MediatR;
using TheSharks.Contracts.Models.Common;
using TheSharks.Contracts.Models.Documents;

namespace TheSharks.Application.Handlers.Documents.Queries.DownloadDocument;

public class DownloadDocumentQuery : BaseIdModel, IRequest<DocumentDownloadResponse>
{

}