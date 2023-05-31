using AutoMapper;
using MediatR;
using TheSharks.Contracts.DataAccess;
using TheSharks.Contracts.Models.Galleries;
using TheSharks.Domain.Entities;

namespace TheSharks.Application.Handlers.Galleries.Queries.GetGallery;

public class GetGalleryQueryHandler : IRequestHandler<GetGalleryQuery, GetGalleryModel>
{
    private readonly IRepository<Gallery> _galleryRepository;
    private readonly IMapper _mapper;

    public GetGalleryQueryHandler(IRepository<Gallery> galleryRepository, IMapper mapper)
    {
        _galleryRepository = galleryRepository;
        _mapper = mapper;
    }

    public async Task<GetGalleryModel> Handle(GetGalleryQuery request, CancellationToken cancellationToken)
    {
        var gallery = await _galleryRepository.Find(x => x.Id.Equals(request.Id), x => x.Pictures);

        return _mapper.Map<GetGalleryModel>(gallery);
    }
}