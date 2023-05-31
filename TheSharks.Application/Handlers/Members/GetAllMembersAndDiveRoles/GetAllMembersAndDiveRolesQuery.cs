using MediatR;
using TheSharks.Contracts.Models.Common;
using TheSharks.Contracts.Models.Members;

namespace TheSharks.Application.Handlers.Members.GetAllMembersAndDiveRoles;

public class GetAllMembersAndDiveRolesQuery : BaseIdModel, IRequest<GetMemberAndDiveLabelsModel>
{

}