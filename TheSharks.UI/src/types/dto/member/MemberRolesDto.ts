import { MemberRoleDto } from "./MemberRoleDto"

export interface MemberRolesDto {
    nonDiveCertificateRoles: MemberRoleDto[]
    diveCertificateRoles: MemberRoleDto[]
}