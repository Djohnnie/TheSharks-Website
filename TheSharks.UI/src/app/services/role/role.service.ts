import { Injectable } from '@angular/core';
import { Claim } from 'src/types/application/role/Claim';
import { ClaimDto } from 'src/types/dto/role/ClaimDto';
import { RoleRestService } from './role-rest.service';
import { response } from '../response';
import { RoleListItem } from 'src/types/application/role/RoleListItem';
import { DiveCertificateDto } from 'src/types/dto/role/DiveCertificateDto';
import { Role } from 'src/types/application/role/Role';

@Injectable({
    providedIn: 'root'
})
export class RoleService {

    constructor(private _rest: RoleRestService) { }

    async getRoles() {
        const o$ = this._rest.getAllRoles()
        return response<RoleListItem[]>(o$)
    }

    async getDiveCertificates() {
        const o$ = this._rest.getDiveCertificates()
        return response<DiveCertificateDto[]>(o$)
    }

    async getRole(id: string) {
        const o$ = this._rest.getRole(id)
        return response<Role>(o$)
    }

    async updateRole(id: string, claims: Claim[]) {
        const o$ = this._rest.putRole({
            id: id,
            claims: claims
        })

        return response(o$)
    }

    async addRole(name: string, concernsDivingCertificate: boolean, claims: Claim[]) {
        const o$ = this._rest.postRole({
            name: name,
            concernsDivingCertificate: concernsDivingCertificate,
            claims: claims.map<ClaimDto>(c => {
                return { ...c }
            })
        })

        return response(o$)
    }

    async deleteRole(id: string) {
        return response(this._rest.deleteRole(id));
    }
}
