using AutoMapper;
using MediatR;
using TheSharks.Contracts.DataAccess;
using TheSharks.Contracts.Models.Galleries;
using TheSharks.Contracts.Services.Statistics;
using TheSharks.Domain.Entities;

namespace TheSharks.Application.Handlers.Galleries.Queries.GetAllGalleries;

public class GetAllGalleriesQueryHandler : IRequestHandler<GetAllGalleriesQuery, GalleryOverviewListModel>
{
    private readonly IRepository<Gallery> _galleryRepository;
    private readonly IStatisticsService _statisticsService;
    private readonly IMapper _mapper;

    public GetAllGalleriesQueryHandler(
        IRepository<Gallery> galleryRepository,
        IStatisticsService statisticsService, IMapper mapper)
    {
        _galleryRepository = galleryRepository;
        _statisticsService = statisticsService;
        _mapper = mapper;
    }

    public async Task<GalleryOverviewListModel> Handle(GetAllGalleriesQuery request, CancellationToken cancellationToken)
    {
        var galleries = await _galleryRepository.GetAllOrderBy(x => x.CreationDate, request.Page, request.RecordsPerPage);
        var records = await _galleryRepository.TableCount();

        await _statisticsService.RecordStatistics("galleries");

        return new GalleryOverviewListModel { Galleries = _mapper.Map<IEnumerable<GalleryOverviewModel>>(galleries), TotalRecords = records };
    }
}