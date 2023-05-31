using MediatR;
using TheSharks.Contracts.Exceptions;
using TheSharks.Contracts.Models.Common;
using TheSharks.Contracts.Services.Email;
using TheSharks.Contracts.Services.Identity;
using TheSharks.Contracts.Services.Members;
using TheSharks.Domain.Entities;

namespace TheSharks.Application.Handlers.Members.SendMail;

public class SendMailQueryHandler : IRequestHandler<SendMailQuery, BaseIdModel>
{
    private readonly IMemberService<Member> _memberService;
    private readonly IRoleService<Role> _roleService;
    private readonly IMailService _mailService;

    public SendMailQueryHandler(IMemberService<Member> memberService, IRoleService<Role> roleService, IMailService mailService)
    {
        _memberService = memberService;
        _roleService = roleService;
        _mailService = mailService;
    }

    public async Task<BaseIdModel> Handle(SendMailQuery request, CancellationToken cancellationToken)
    {
        if (!request.CheckedRecipients.Any() && !request.CheckedRoles.Any()) throw new AppException("Gelieve een persoon of groep aan te duiden");

        var sender = await _memberService.Find(request.SenderId);
        if (sender == null) throw new AppException("Gelieve in te loggen");

        var recipients = new List<Member> { sender };

        foreach (var role in await _roleService.GetAllRoles(x => request.CheckedRoles.Contains(x.Id)))
        {
            recipients.AddRange(await _memberService.GetAllInRole(role.Name));
        }

        // Prevent sending double emails to members who have both been selected manually but were also in a checked role
        var remainingIds = request.CheckedRecipients.Except(recipients.Select(x => x.Id));
        recipients.AddRange(await _memberService.GetAll(x => remainingIds.Contains(x.Id)));

        var senderName = $"{sender.FirstName} {sender.LastName}";

        await _mailService.SendEmail(sender.Email, senderName, recipients.Select(x => x.Email).Distinct(), request.Subject, request.Message);

        return new BaseIdModel { Id = request.Id };
    }
}