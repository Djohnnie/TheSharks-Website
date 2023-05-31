using MediatR;
using TheSharks.Contracts.DataAccess;
using TheSharks.Contracts.Models.Common;
using TheSharks.Contracts.Services.Storage;
using TheSharks.Domain.Entities;

namespace TheSharks.Application.Handlers.Galleries.Queries.RemoveGallery;

public class RemoveGalleryQueryHandler : IRequestHandler<RemoveGalleryQuery, BaseIdModel>
{

    private readonly IRepository<Gallery> _galleryRepository;
    private readonly IPictureStorageService _storageService;

    public RemoveGalleryQueryHandler(IRepository<Gallery> galleryRepository, IPictureStorageService storageService)
    {
        _galleryRepository = galleryRepository;
        _storageService = storageService;
    }

    public async Task<BaseIdModel> Handle(RemoveGalleryQuery request, CancellationToken cancellationToken)
    {
        var gallery = await _galleryRepository.Find(x => x.Id.Equals(request.Id), x => x.Pictures);

        await Task.Run(() => _storageService.Delete(gallery.Pictures.Select(x => x.Name)));

        var deleteResult = await _galleryRepository.Delete(request.Id);
        return new BaseIdModel { Id = deleteResult.Id };
    }
}