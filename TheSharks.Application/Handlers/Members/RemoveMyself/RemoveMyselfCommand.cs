using MediatR;

namespace TheSharks.Application.Handlers.Members.RemoveMyself;

public class RemoveMyselfCommand : IRequest
{
    public Guid Id { get; set; }
}