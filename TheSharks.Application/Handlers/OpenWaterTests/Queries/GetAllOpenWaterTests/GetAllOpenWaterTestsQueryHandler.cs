using MediatR;
using TheSharks.Contracts.DataAccess;
using TheSharks.Contracts.Models.OpenWaterTests;
using TheSharks.Domain.Entities;

namespace TheSharks.Application.Handlers.OpenWaterTests.Queries.GetAllOpenWaterTests;

public class GetAllOpenWaterTestsQueryHandler : IRequestHandler<GetAllOpenWaterTestsQuery, OpenWaterTestsOverviewListModel>
{
    private readonly IRepository<OpenWaterTest> _openWaterTestRepository;

    public GetAllOpenWaterTestsQueryHandler(
        IRepository<OpenWaterTest> openWaterTestRepository)
    {
        _openWaterTestRepository = openWaterTestRepository;
    }

    public async Task<OpenWaterTestsOverviewListModel> Handle(GetAllOpenWaterTestsQuery request, CancellationToken cancellationToken)
    {
        var overviewListModel = new OpenWaterTestsOverviewListModel
        {
            OpenWaterTests = new List<OpenWaterTestModel>()
        };

        var openWaterTests = await _openWaterTestRepository.GetAll();

        if (string.IsNullOrEmpty(request.DiveCertificate))
        {
            overviewListModel.OpenWaterTests.AddRange(openWaterTests.Select(Map()));
        }
        else
        {
            overviewListModel.OpenWaterTests.AddRange(
                openWaterTests.Where(x => x.DiveCertificate == request.DiveCertificate).Select(Map()));

            overviewListModel.OpenWaterTests.AddRange(
                openWaterTests.Where(x => x.DiveCertificate != request.DiveCertificate).Select(Map()));
        }

        return overviewListModel;
    }

    private Func<OpenWaterTest, OpenWaterTestModel> Map()
    {
        return x => new OpenWaterTestModel
        {
            Id = x.Id,
            Title = x.Title,
            DiveCertificate = x.DiveCertificate,
            Content = x.Content
        };
    }
}