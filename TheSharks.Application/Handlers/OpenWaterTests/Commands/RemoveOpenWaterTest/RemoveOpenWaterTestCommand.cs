using MediatR;

namespace TheSharks.Application.Handlers.OpenWaterTests.Commands.RemoveOpenWaterTest;

public class RemoveOpenWaterTestCommand : IRequest
{
    public Guid Id { get; set; }
}