using MediatR;
using TheSharks.Contracts.DataAccess;
using TheSharks.Contracts.Models.Common;
using TheSharks.Domain.Entities;

namespace TheSharks.Application.Handlers.OpenWaterTests.Commands.AddOpenWaterTest;

public class AddOpenWaterTestCommandHandler : IRequestHandler<AddOpenWaterTestCommand, BaseIdModel>
{
    private readonly IRepository<OpenWaterTest> _openWaterTestRepository;

    public AddOpenWaterTestCommandHandler(IRepository<OpenWaterTest> openWaterTestRepository)
    {
        _openWaterTestRepository = openWaterTestRepository;
    }

    public async Task<BaseIdModel> Handle(AddOpenWaterTestCommand request, CancellationToken cancellationToken)
    {
        var entity = new OpenWaterTest
        {
            Title = request.Title,
            DiveCertificate = request.DiveCertificate,
            Content = request.Content
        };

        var saveResult = await _openWaterTestRepository.Add(entity);

        return new BaseIdModel { Id = saveResult.Id };
    }
}