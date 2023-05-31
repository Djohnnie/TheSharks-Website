using MediatR;
using TheSharks.Contracts.DataAccess;
using TheSharks.Contracts.Models.Documents;
using TheSharks.Contracts.Services.Statistics;
using TheSharks.Domain.Entities;

namespace TheSharks.Application.Handlers.Documents.Queries.GetAllDocuments;

public class GetAllDocumentsQueryHandler : IRequestHandler<GetAllDocumentsQuery, DocumentOverviewModel>
{
    private readonly IRepository<Document> _documentRepository;
    private readonly IStatisticsService _statisticsService;

    public GetAllDocumentsQueryHandler(
        IRepository<Document> documentRepository,
        IStatisticsService statisticsService)
    {
        _documentRepository = documentRepository;
        _statisticsService = statisticsService;
    }

    public async Task<DocumentOverviewModel> Handle(GetAllDocumentsQuery request, CancellationToken cancellationToken)
    {
        var documents = await _documentRepository.GetAllOrderBy(x => x.CreationDate, request.Page, request.RecordsPerPage);
        var totalRecords = await _documentRepository.TableCount();

        var importantDocuments = documents.Where(x => x.IsImportant).OrderBy(x => x.Name).ToList();
        var otherDocuments = documents.Where(x => !x.IsImportant).OrderByDescending(x => x.CreationDate).ToList();

        var documentsToReturn = new List<DocumentModel>();
        Func<Document, DocumentModel> documentSelector = d => new DocumentModel { Id = d.Id, Name = d.Name, FileName = d.FileName, IsImportant = d.IsImportant };
        documentsToReturn.AddRange(importantDocuments.Select(documentSelector));
        documentsToReturn.AddRange(otherDocuments.Select(documentSelector));
        
        await _statisticsService.RecordStatistics("documents");

        return new DocumentOverviewModel { Documents = documentsToReturn, TotalRecords = totalRecords };
    }
}