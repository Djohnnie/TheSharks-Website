using MediatR;
using TheSharks.Contracts.Models.OpenWaterTests;

namespace TheSharks.Application.Handlers.OpenWaterTests.Queries.GetOpenWaterTest;

public class GetOpenWaterTestQuery : IRequest<OpenWaterTestModel>
{
    public Guid Id { get; set; }
}