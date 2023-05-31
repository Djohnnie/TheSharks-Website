using MediatR;
using TheSharks.Contracts.DataAccess;
using TheSharks.Contracts.Models.Common;
using TheSharks.Domain.Entities;

namespace TheSharks.Application.Handlers.OpenWaterTests.Commands.UpdateOpenWaterTest;

public class UpdateOpenWaterTestCommandHandler : IRequestHandler<UpdateOpenWaterTestCommand, BaseIdModel>
{
    private readonly IRepository<OpenWaterTest> _openWaterTestRepository;

    public UpdateOpenWaterTestCommandHandler(IRepository<OpenWaterTest> openWaterTestRepository)
    {
        _openWaterTestRepository = openWaterTestRepository;
    }

    public async Task<BaseIdModel> Handle(UpdateOpenWaterTestCommand request, CancellationToken cancellationToken)
    {
        var entity = await _openWaterTestRepository.Find(x => x.Id == request.Id);

        entity.Title = request.Title;
        entity.DiveCertificate = request.DiveCertificate;
        entity.Content = request.Content;

        var saveResult = await _openWaterTestRepository.Update(entity);

        return new BaseIdModel { Id = saveResult.Id };
    }
}