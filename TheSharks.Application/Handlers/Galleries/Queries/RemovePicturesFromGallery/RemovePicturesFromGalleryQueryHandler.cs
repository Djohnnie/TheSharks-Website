using MediatR;
using TheSharks.Contracts.DataAccess;
using TheSharks.Contracts.Models.Common;
using TheSharks.Contracts.Services.Storage;
using TheSharks.Domain.Entities;

namespace TheSharks.Application.Handlers.Galleries.Queries.RemovePicturesFromGallery;

public class RemovePicturesFromGalleryQueryHandler : IRequestHandler<RemovePicturesFromGalleryQuery, BaseIdModel>
{
    private readonly IRepository<Gallery> _galleryRepository;
    private readonly IPictureStorageService _storageService;
    private readonly IRepository<Picture> _pictureRepository;

    public RemovePicturesFromGalleryQueryHandler(IPictureStorageService storageService, IRepository<Picture> pictureRepository, IRepository<Gallery> galleryRepository)
    {
        _storageService = storageService;
        _pictureRepository = pictureRepository;
        _galleryRepository = galleryRepository;
    }

    public async Task<BaseIdModel> Handle(RemovePicturesFromGalleryQuery request, CancellationToken cancellationToken)
    {
        await Task.Run(() => _storageService.Delete(request.Pictures.Select(x => x.Name)));

        var deleteResult = await _pictureRepository.Delete(x => request.Pictures.Select(x => x.Id).Contains(x.Id));

        var gallery = await _galleryRepository.Find(x => x.Id.Equals(request.Id), x => x.Pictures);

        if (gallery.Pictures.Count > 0) gallery.UrlFirstPicture = gallery.Pictures[0].StorageUrl;
        else gallery.UrlFirstPicture = null;

        gallery.AmountPictures -= deleteResult.Count();
        await _galleryRepository.Update(gallery);

        return new BaseIdModel { Id = gallery.Id };
    }
}