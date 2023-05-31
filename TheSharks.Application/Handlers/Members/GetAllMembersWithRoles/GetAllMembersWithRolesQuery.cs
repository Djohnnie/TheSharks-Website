using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheSharks.Contracts.Models.Members;

namespace TheSharks.Application.Handlers.Members.GetAllMembersWithRoles;

public class GetAllMembersWithRolesQuery : IRequest<MemberRolesOverviewListModel>
{
}