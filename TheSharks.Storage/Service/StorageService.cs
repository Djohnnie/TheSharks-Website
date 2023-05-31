using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Http;
using TheSharks.Contracts.Exceptions;
using TheSharks.Contracts.Models.Storage;
using TheSharks.Contracts.Services.Storage;
using TheSharks.Storage.Options;

namespace TheSharks.Storage.Service;

public class StorageService : IStorageService
{
    private readonly StorageItemOptions _options;

    public StorageService(StorageItemOptions options)
    {
        _options = options;
    }

    private BlobContainerClient CreateContainer()
    {
        BlobClientOptions options = new BlobClientOptions(BlobClientOptions.ServiceVersion.V2019_02_02);
        return new BlobContainerClient(_options.StorageConnectionString, _options.ContainerName, options);
    }

    public async Task<StorageModel> Upload(IFormFile file)
    {
        try
        {
            BlobClient client = CreateContainer().GetBlobClient(file.FileName);
            await using (Stream? data = file.OpenReadStream())
            {
                await client.UploadAsync(data);
            };

            return new StorageModel { Uri = client.Uri.AbsoluteUri, Name = client.Name };
        }
        catch (RequestFailedException ex)
        {
            if (ex.ErrorCode == BlobErrorCode.BlobAlreadyExists) throw new StorageException("File bestaat al");
            else throw new StorageException("Onverwachte error. Probeer opnieuw");
        }
    }

    public async Task<StorageModel> Download(string fileName)
    {
        BlobClient file = CreateContainer().GetBlobClient(fileName);
        if (await file.ExistsAsync())
        {
            var data = await file.OpenReadAsync();
            Stream blobContent = data;

            var content = await file.DownloadContentAsync();

            return new StorageModel { Content = blobContent, ContentType = content.Value.Details.ContentType, Uri = file.Uri.AbsoluteUri };
        }
        else throw new StorageException($"File {fileName} kon niet gevonden worden");
    }

    public async Task Delete(IEnumerable<string> fileNames)
    {
        try
        {
            foreach (string fileName in fileNames)
            {
                BlobClient file = CreateContainer().GetBlobClient(fileName);
                await file.DeleteAsync();
            }
        }
        catch (RequestFailedException ex)
        {
            if (ex.ErrorCode == BlobErrorCode.BlobAlreadyExists) throw new StorageException("File bestaat niet");
            else throw new StorageException("Onverwachte error. Probeer opnieuw");
        }
    }

    public async Task Delete(string fileName)
    {
        try
        {
            BlobClient file = CreateContainer().GetBlobClient(fileName);
            await file.DeleteAsync();
        }
        catch (RequestFailedException ex)
        {
            if (ex.ErrorCode == BlobErrorCode.BlobAlreadyExists) throw new StorageException("File bestaat niet");
            else throw new StorageException("Onverwachte error. Probeer opnieuw");
        }
    }

    public async Task<IEnumerable<StorageModel>> Upload(IEnumerable<IFormFile> files)
    {
        var models = new List<StorageModel>();
        try
        {
            foreach (IFormFile file in files)
            {
                BlobClient client = CreateContainer().GetBlobClient(file.FileName);
                await using (Stream? data = file.OpenReadStream())
                {
                    await client.UploadAsync(data);
                };
                models.Add(new StorageModel { Uri = client.Uri.AbsoluteUri, Name = file.FileName });
            }
            return models;
        }
        catch (RequestFailedException ex)
        {
            if (ex.ErrorCode == BlobErrorCode.BlobAlreadyExists) throw new StorageException("File bestaat al");
            else throw new StorageException("Onverwachte error. Probeer opnieuw");
        }
    }
}