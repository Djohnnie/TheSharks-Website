using MediatR;
using TheSharks.Contracts.DataAccess;
using TheSharks.Contracts.Exceptions;
using TheSharks.Contracts.Models.Common;
using TheSharks.Contracts.Services.Storage;
using TheSharks.Domain.Common;
using TheSharks.Domain.Entities;

namespace TheSharks.Application.Handlers.Galleries.Queries.AddPictureToGallery;

public class AddPictureToGalleryQueryHandler : IRequestHandler<AddPictureToGalleryQuery, BaseIdModel>
{
    private readonly IRepository<Picture> _pictureRepository;
    private readonly IPictureStorageService _storageService;
    private readonly IRepository<Gallery> _galleryRepository;

    public AddPictureToGalleryQueryHandler(IRepository<Picture> pictureRepository, IPictureStorageService storageService, IRepository<Gallery> galleryRepository)
    {
        _pictureRepository = pictureRepository;
        _storageService = storageService;
        _galleryRepository = galleryRepository;
    }

    public async Task<BaseIdModel> Handle(AddPictureToGalleryQuery request, CancellationToken cancellationToken)
    {
        if (request.Pictures.Any(x => Path.GetExtension(x.FileName) == null || !Enum.GetNames(typeof(ImageExtension)).Select(x => "." + x.ToLower()).Contains(Path.GetExtension(x.FileName).ToLower())))
            throw new AppException("Moet een .jpg | .jpeg | .png bestand zijn");
        if (request.Pictures.Count() != request.Pictures.Select(x => x.FileName).Distinct().Count())
            throw new AppException("Je hebt dubbele foto's geselecteerd!");

        var gallery = await _galleryRepository.Find(x => x.Id.Equals(request.Id));

        var uploadedPictures = await _storageService.Upload(request.Pictures);

        var toSavePictures = new List<Picture>(uploadedPictures.Select(x => new Picture { Name = x.Name, Gallery = gallery, StorageUrl = x.Uri }));

        var saveResult = await _pictureRepository.Add(toSavePictures);

        gallery.AmountPictures += toSavePictures.Count;
        if (gallery.UrlFirstPicture == null) gallery.UrlFirstPicture = toSavePictures[0].StorageUrl;
        await _galleryRepository.Update(gallery);

        return new BaseIdModel { Id = request.Id };
    }
}