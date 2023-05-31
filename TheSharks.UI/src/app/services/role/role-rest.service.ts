import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs';
import { environment } from 'src/environments/environment';
import { AddRoleDto } from 'src/types/dto/role/AddRoleDto';
import { RoleDto } from 'src/types/dto/role/RoleDto';
import { RoleListDto } from 'src/types/dto/role/RoleListDto';
import { UpdateRoleDto } from 'src/types/dto/role/UpdateRoleDto';
import { GetDiveCertificatesDto } from 'src/types/dto/role/GetDiveCertificatesDto';
import { DiveCertificateDto } from 'src/types/dto/role/DiveCertificateDto';
import { Role } from 'src/types/application/role/Role';
import { RoleListItem } from 'src/types/application/role/RoleListItem';

@Injectable({
    providedIn: 'root'
})
export class RoleRestService {

    constructor(private _http: HttpClient) { }

    getAllRoles() {
        return this._http.get<RoleListDto>(`${environment.baseUrl}/api/Role`).pipe(
            map<RoleListDto, RoleListItem[]>((r) => {
                const d = r.diveCertificateRoles.map<RoleListItem>(dcr => {
                    return { ...dcr, diveCertificateRole: true }
                })

                const nd = r.nonDiveCertificateRoles.map<RoleListItem>(dcr => {
                    return { ...dcr, diveCertificateRole: false }
                })

                return [...d, ...nd]
            })
        )
    }

    getDiveCertificates() {
        return this._http.get<GetDiveCertificatesDto>(`${environment.baseUrl}/api/Role/divecertificates`).pipe(
            map<GetDiveCertificatesDto, DiveCertificateDto[]>((r) => {
                const d = r.diveCertificates.map<DiveCertificateDto>(dc => {
                    return { ...dc }
                });

                return [...d]
            })
        )
    }

    getRole(id: string) {
        return this._http.get<RoleDto>(`${environment.baseUrl}/api/Role/${id}`).pipe(
            map<RoleDto, Role>((r) => {
                return { ...r }
            })
        )
    }

    postRole(dto: AddRoleDto) {
        return this._http.post(`${environment.baseUrl}/api/Role`, dto)
    }

    putRole(dto: UpdateRoleDto) {
        return this._http.put(`${environment.baseUrl}/api/Role/${dto.id}`, dto)
    }

    deleteRole(id: string) {
        return this._http.delete(`${environment.baseUrl}/api/Role/${id}`)
    }
}
