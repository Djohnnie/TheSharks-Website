using MediatR;
using TheSharks.Contracts.DataAccess;
using TheSharks.Contracts.Models.Common;
using TheSharks.Domain.Entities;

namespace TheSharks.Application.Handlers.Galleries.Queries.AddGallery;

public class AddGalleryQueryHandler : IRequestHandler<AddGalleryQuery, BaseIdModel>
{
    private readonly IRepository<Gallery> _galleryRepository;

    public AddGalleryQueryHandler(IRepository<Gallery> galleryRepository)
    {
        _galleryRepository = galleryRepository;
    }

    public async Task<BaseIdModel> Handle(AddGalleryQuery request, CancellationToken cancellationToken)
    {
        var result = await _galleryRepository.Add(new Gallery { Name = request.Name });
        return new BaseIdModel { Id = result.Id };
    }
}