using MediatR;
using TheSharks.Contracts.Models.Common;

namespace TheSharks.Application.Handlers.Members.SendMail;

public class SendMailQuery : BaseIdModel, IRequest<BaseIdModel>
{
    public string Subject { get; set; }
    public string Message { get; set; }
    public Guid SenderId { get; set; }
    public IEnumerable<Guid> CheckedRecipients { get; set; } = Enumerable.Empty<Guid>();
    public IEnumerable<Guid> CheckedRoles { get; set; } = Enumerable.Empty<Guid>();
}