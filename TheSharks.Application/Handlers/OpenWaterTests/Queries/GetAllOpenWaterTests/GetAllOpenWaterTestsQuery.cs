using MediatR;
using TheSharks.Contracts.Models.OpenWaterTests;

namespace TheSharks.Application.Handlers.OpenWaterTests.Queries.GetAllOpenWaterTests;

public class GetAllOpenWaterTestsQuery : IRequest<OpenWaterTestsOverviewListModel>
{
    public string DiveCertificate { get; set; }
}