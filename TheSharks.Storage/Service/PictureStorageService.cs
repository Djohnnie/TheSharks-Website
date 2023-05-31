using Microsoft.Extensions.Options;
using TheSharks.Contracts.Services.Storage;
using TheSharks.Storage.Options;

namespace TheSharks.Storage.Service;

public class PictureStorageService : StorageService, IPictureStorageService
{
    public PictureStorageService(IOptions<StorageOptions> options) : base(options.Value.Pictures) { }
}