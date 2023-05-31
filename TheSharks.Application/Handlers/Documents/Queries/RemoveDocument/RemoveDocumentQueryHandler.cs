using MediatR;
using TheSharks.Contracts.DataAccess;
using TheSharks.Contracts.Models.Common;
using TheSharks.Contracts.Services.Storage;
using TheSharks.Domain.Entities;

namespace TheSharks.Application.Handlers.Documents.Queries.RemoveDocument;

public class RemoveDocumentQueryHandler : IRequestHandler<RemoveDocumentQuery, BaseIdModel>
{
    private readonly IRepository<Document> _documentRepository;
    private readonly IDocumentStorageService _storageService;

    public RemoveDocumentQueryHandler(IRepository<Document> documentRepository, IDocumentStorageService storageService)
    {
        _documentRepository = documentRepository;
        _storageService = storageService;
    }

    public async Task<BaseIdModel> Handle(RemoveDocumentQuery request, CancellationToken cancellationToken)
    {
        await Task.Run(() => _storageService.Delete(request.FileName));

        var result = await _documentRepository.Delete(request.Id);

        return new BaseIdModel { Id = result.Id };
    }
}