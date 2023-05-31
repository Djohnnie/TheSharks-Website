using MediatR;
using TheSharks.Domain.Entities;

namespace TheSharks.Application.Handlers.Members.ResetPassword;

public class ResetPasswordQuery : IRequest<Member>
{
    public Guid Id { get; set; }
    public string Token { get; set; }
    public string NewPassword { get; set; }
}