import { Injectable } from '@angular/core';
import { MemberListAndDiveRoles } from 'src/types/application/member/MemberListAndDiveRoles';
import { MemberRestService } from './member-rest.service';
import { MemberListItem } from 'src/types/application/member/MemberListItem';
import { MemberRoles } from 'src/types/application/role/MemberRoles';
import { RoleUpdate } from 'src/types/application/role/RoleUpdate';
import { response } from '../response';
import { MemberListItemWithRoles } from 'src/types/application/member/MemberListItemWithRoles';
import { MemberItem } from 'src/types/application/member/MemberItem';

@Injectable({
    providedIn: 'root'
})
export class MemberService {

    constructor(private _rest: MemberRestService) {

    }

    async getAllMembers() {
        const o$ = this._rest.getAllMembers()
        return response<MemberListItem[]>(o$)
    }

    async getMember(id: string) {
        const o$ = this._rest.getMember(id)
        return response<MemberItem>(o$)
    }

    async editMember(id: string, firstName: string, lastName: string, userName: string, email: string) {
        const o$ = this._rest.putEditMember(id,{
            id: id,
            firstName: firstName,
            lastName: lastName,
            userName: userName,
            email: email
        })

        return response(o$)
    }

    async getMembersAndDiveLabels(registratorId: string) {
        const o$ = this._rest.getAllMembersAndDiveLabels(registratorId)
        return response<MemberListAndDiveRoles>(o$)
    }

    async getMemberRoles(id: string) {
        const o$ = this._rest.getMemberRoles(id)
        return response<MemberRoles>(o$)
    }

    async updateMemberRoles(memberId: string, certificate: RoleUpdate, roles: RoleUpdate[]) {
        const o$ = this._rest.putMemberRoles({
            memberId: memberId,
            diveCertificateRole: certificate,
            regularRoles: roles
        })

        return response(o$)
    }

    async sendEmail(id: string, senderId: string, subject: string, message: string, checkedRecipients: string[], checkedRoles: string[]) {
        const o$ = this._rest.postSendEmail({
            id: id,
            senderId: senderId,
            subject: subject,
            message: message,
            checkedRecipients: checkedRecipients,
            checkedRoles: checkedRoles
        })

        return response(o$)
    }

    async getProfilePicture(id: string) {
        return response(this._rest.getProfilePictire(id))
    }

    async getAllMembersWithRoles() {
        return response<MemberListItemWithRoles[]>(this._rest.getAllMembersWithRoles())
    }

    async deleteMember(id: string) {
        const o$ = this._rest.deleteMember(id)
        return response(o$)
    }

    async deleteMyself(id: string) {
        const o$ = this._rest.deleteMyself(id)
        return response(o$)
    }
}
