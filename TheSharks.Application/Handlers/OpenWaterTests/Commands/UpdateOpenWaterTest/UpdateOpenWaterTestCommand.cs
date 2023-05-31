using MediatR;
using TheSharks.Contracts.Models.Common;

namespace TheSharks.Application.Handlers.OpenWaterTests.Commands.UpdateOpenWaterTest;

public class UpdateOpenWaterTestCommand : IRequest<BaseIdModel>
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string DiveCertificate { get; set; }
    public string Content { get; set; }
}