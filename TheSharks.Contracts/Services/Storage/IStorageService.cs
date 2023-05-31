using Microsoft.AspNetCore.Http;
using TheSharks.Contracts.Models.Storage;

namespace TheSharks.Contracts.Services.Storage;

public interface IStorageService
{
    Task<StorageModel> Upload(IFormFile file);
    Task<IEnumerable<StorageModel>> Upload(IEnumerable<IFormFile> files);
    Task<StorageModel> Download(string fileName);
    Task Delete(IEnumerable<string> fileNames);
    Task Delete(string fileName);
}