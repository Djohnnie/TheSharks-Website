using MediatR;
using TheSharks.Application.Handlers.OpenWaterTests.Queries.GetAllOpenWaterTests;
using TheSharks.Contracts.DataAccess;
using TheSharks.Contracts.Models.OpenWaterTests;
using TheSharks.Domain.Entities;

namespace TheSharks.Application.Handlers.OpenWaterTests.Queries.GetOpenWaterTest;

public class GetOpenWaterTestQueryHandler : IRequestHandler<GetOpenWaterTestQuery, OpenWaterTestModel>
{
    private readonly IRepository<OpenWaterTest> _openWaterTestRepository;

    public GetOpenWaterTestQueryHandler(
        IRepository<OpenWaterTest> openWaterTestRepository)
    {
        _openWaterTestRepository = openWaterTestRepository;
    }

    public async Task<OpenWaterTestModel> Handle(GetOpenWaterTestQuery request, CancellationToken cancellationToken)
    {
        var openWaterTests = await _openWaterTestRepository.Find(x => x.Id == request.Id);
        return new OpenWaterTestModel
        {
            Id = openWaterTests.Id,
            Title = openWaterTests.Title,
            DiveCertificate = openWaterTests.DiveCertificate,
            Content = openWaterTests.Content
        };
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