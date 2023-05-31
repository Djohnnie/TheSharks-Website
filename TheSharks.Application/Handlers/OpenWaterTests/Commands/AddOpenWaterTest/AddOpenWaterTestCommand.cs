using MediatR;
using TheSharks.Contracts.Models.Common;

namespace TheSharks.Application.Handlers.OpenWaterTests.Commands.AddOpenWaterTest;

public class AddOpenWaterTestCommand : IRequest<BaseIdModel>
{
    public string Title { get; set; }
    public string DiveCertificate { get; set; }
    public string Content { get; set; }
}