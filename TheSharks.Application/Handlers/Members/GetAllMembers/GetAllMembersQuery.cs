using MediatR;
using TheSharks.Contracts.Models.Members;

namespace TheSharks.Application.Handlers.Members.GetAllMembers;

public class GetAllMembersQuery : IRequest<MemberOverviewListModel>
{
}