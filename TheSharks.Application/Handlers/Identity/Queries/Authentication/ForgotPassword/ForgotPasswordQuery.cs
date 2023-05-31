using MediatR;
using TheSharks.Domain.Entities;

namespace TheSharks.Application.Handlers.Identity.Queries.Authentication.ForgotPassword;

public class ForgotPasswordQuery : IRequest<Member>
{
    public string Email { get; set; }
}