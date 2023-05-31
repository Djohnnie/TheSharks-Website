import { RoleUpdateDto } from "./RoleUpdateDto";

export interface UpdateMemberRolesDto {
    memberId: string;
    diveCertificateRole: RoleUpdateDto;
    regularRoles: RoleUpdateDto[];
}