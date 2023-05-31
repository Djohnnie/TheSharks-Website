using MediatR;
using TheSharks.Domain.Entities;

namespace TheSharks.Application.Handlers.Identity.Queries.Authentication.Register;

public class RegisterQuery : IRequest<Member>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
}