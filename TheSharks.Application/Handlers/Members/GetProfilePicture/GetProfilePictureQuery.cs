using MediatR;
using TheSharks.Contracts.Models.Common;
using TheSharks.Contracts.Models.Members;

namespace TheSharks.Application.Handlers.Members.GetProfilePicture;

public class GetProfilePictureQuery : BaseIdModel, IRequest<ProfilePictureModel>
{

}