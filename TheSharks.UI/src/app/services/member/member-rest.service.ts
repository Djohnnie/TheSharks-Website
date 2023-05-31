import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs';
import { environment } from 'src/environments/environment';
import { MemberListAndDiveRolesDto } from 'src/types/dto/member/MemberListAndDiveRolesDto';
import { MemberListDto } from 'src/types/dto/member/MemberListDto';
import { MemberRoleDto } from 'src/types/dto/member/MemberRoleDto';
import { MemberRolesDto } from 'src/types/dto/member/MemberRolesDto';
import { UpdateMemberRolesDto } from 'src/types/dto/member/UpdateMemberRolesDto';
import { MemberRole } from 'src/types/application/role/MemberRole';
import { MemberListItem } from 'src/types/application/member/MemberListItem';
import { MemberRoles } from 'src/types/application/role/MemberRoles';
import { SendEmailDto } from 'src/types/dto/member/SendEmailDto';
import { ProfilePictureDto } from 'src/types/dto/member/ProfilePictureDto';
import { MemberListWithRoles } from 'src/types/application/member/MemberListWithRoles';
import { MemberListItemWithRoles } from 'src/types/application/member/MemberListItemWithRoles';
import { MemberItem } from 'src/types/application/member/MemberItem';
import { EditMemberDto } from 'src/types/dto/member/EditMemberDto';

@Injectable({
    providedIn: 'root'
})
export class MemberRestService {

    constructor(private _http: HttpClient) {

    }

    getAllMembers() {
        return this._http.get<MemberListDto>(`${environment.baseUrl}/api/Member`).pipe(
            map<MemberListDto, MemberListItem[]>((v) => {
                return v.members
            }),
            map<MemberListItem[], MemberListItem[]>((v) => {
                return v.map((m) => {
                    return { ...m }
                })
            })
        )
    }

    getMember(id: string) {
        return this._http.get<MemberItem>(`${environment.baseUrl}/api/Member/${id}`).pipe()
    }

    putEditMember(id: string, dto: EditMemberDto) {
        return this._http.put(`${environment.baseUrl}/api/Member/${id}/edit`, dto)
    }

    getAllMembersAndDiveLabels(registratorId: string) {
        return this._http.get<MemberListAndDiveRolesDto>(`${environment.baseUrl}/api/Member/AndDiveLabels?RegistratorId=${registratorId}`)
    }

    getAllMembersWithRoles() {
        return this._http.get<MemberListWithRoles>(`${environment.baseUrl}/api/Member/AndRoles`).pipe(
            map<MemberListWithRoles, MemberListItemWithRoles[]>(l => l.members)
        )
    }

    getMemberRoles(id: string) {
        return this._http.get<MemberRolesDto>(`${environment.baseUrl}/api/Member/${id}/roles`).pipe(
            map<MemberRolesDto, MemberRoles>((mr) => mr)
        )
    }

    putMemberRoles(dto: UpdateMemberRolesDto) {
        return this._http.put(`${environment.baseUrl}/api/Member/${dto.memberId}/roles`, dto)
    }

    postSendEmail(dto: SendEmailDto) {
        return this._http.post(`${environment.baseUrl}/api/Member/send-mail`, dto)
    }

    getProfilePictire(id: string) {
        return this._http.get<ProfilePictureDto>(`${environment.baseUrl}/api/Member/${id}/profile-picture`).pipe(
            map<ProfilePictureDto, string>(v => v.picture)
        )
    }

    deleteMember(id: string) {
        return this._http.delete(`${environment.baseUrl}/api/Member/${id}`)
    }

    deleteMyself(id: string) {
        return this._http.delete(`${environment.baseUrl}/api/Member/me/${id}`)
    }
}