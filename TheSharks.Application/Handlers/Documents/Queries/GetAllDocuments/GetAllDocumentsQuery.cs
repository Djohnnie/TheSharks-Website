using MediatR;
using TheSharks.Contracts.Models.Documents;
using TheSharks.Contracts.Models.Pagination;

namespace TheSharks.Application.Handlers.Documents.Queries.GetAllDocuments;

public class GetAllDocumentsQuery : PaginationBaseModel, IRequest<DocumentOverviewModel>
{
}