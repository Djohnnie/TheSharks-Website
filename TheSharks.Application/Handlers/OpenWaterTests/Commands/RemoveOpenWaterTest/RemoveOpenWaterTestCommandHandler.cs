using MediatR;
using TheSharks.Contracts.DataAccess;
using TheSharks.Domain.Entities;

namespace TheSharks.Application.Handlers.OpenWaterTests.Commands.RemoveOpenWaterTest;

public class RemoveOpenWaterTestCommandHandler : IRequestHandler<RemoveOpenWaterTestCommand>
{
    private readonly IRepository<OpenWaterTest> _openWaterTestRepository;

    public RemoveOpenWaterTestCommandHandler(IRepository<OpenWaterTest> openWaterTestRepository)
    {
        _openWaterTestRepository = openWaterTestRepository;
    }

    public async Task<Unit> Handle(RemoveOpenWaterTestCommand request, CancellationToken cancellationToken)
    {
        await _openWaterTestRepository.Delete(request.Id);

        return Unit.Value;
    }
}