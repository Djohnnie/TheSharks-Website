using Microsoft.Extensions.Options;
using TheSharks.Contracts.Services.Storage;
using TheSharks.Storage.Options;

namespace TheSharks.Storage.Service;

public class DocumentStorageService : StorageService, IDocumentStorageService
{
    public DocumentStorageService(IOptions<StorageOptions> options) : base(options.Value.Documents) { }
}