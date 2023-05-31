using MediatR;
using TheSharks.Contracts.DataAccess;
using TheSharks.Contracts.Models.Documents;
using TheSharks.Contracts.Services.Storage;
using TheSharks.Domain.Entities;


namespace TheSharks.Application.Handlers.Documents.Queries.DownloadDocument;

public class DownloadDocumentQueryHandler : IRequestHandler<DownloadDocumentQuery, DocumentDownloadResponse>
{
    private readonly IRepository<Document> _documentRepository;
    private readonly IDocumentStorageService _storageService;

    public DownloadDocumentQueryHandler(IRepository<Document> documentRepository, IDocumentStorageService storageService)
    {
        _documentRepository = documentRepository;
        _storageService = storageService;
    }

    public async Task<DocumentDownloadResponse> Handle(DownloadDocumentQuery request, CancellationToken cancellationToken)
    {
        var document = await _documentRepository.Find(x => x.Id.Equals(request.Id));
        var documentMetaData = await _storageService.Download(document.FileName);
        
        return new DocumentDownloadResponse { Uri = documentMetaData.Uri };
    }
}