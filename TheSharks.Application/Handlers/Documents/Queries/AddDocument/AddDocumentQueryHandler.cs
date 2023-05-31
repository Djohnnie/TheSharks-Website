using MediatR;
using TheSharks.Contracts.DataAccess;
using TheSharks.Contracts.Exceptions;
using TheSharks.Contracts.Models.Common;
using TheSharks.Contracts.Services.Storage;
using TheSharks.Domain.Common;
using TheSharks.Domain.Entities;

namespace TheSharks.Application.Handlers.Documents.Queries.AddDocument;

public class AddDocumentQueryHandler : IRequestHandler<AddDocumentQuery, BaseIdModel>
{
    private readonly IRepository<Document> _documentRepository;
    private readonly IDocumentStorageService _storageService;

    public AddDocumentQueryHandler(IRepository<Document> documentRepository, IDocumentStorageService storageService)
    {
        _documentRepository = documentRepository;
        _storageService = storageService;
    }

    public async Task<BaseIdModel> Handle(AddDocumentQuery request, CancellationToken cancellationToken)
    {
        string extension = Path.GetExtension(request.File.FileName);
        if (extension == null || !Enum.GetNames(typeof(DocumentExtension)).Select(x => "." + x.ToLower()).Contains(extension.ToLower())) throw new AppException("Moet een .docx | .doc | .pdf bestand zijn");

        var uploadResult = await _storageService.Upload(request.File);
        var saveResult = await _documentRepository.Add(new Document { Name = request.Name, Url = uploadResult.Uri, FileName = uploadResult.Name, IsImportant = request.IsImportant });

        return new BaseIdModel { Id = saveResult.Id };
    }
}