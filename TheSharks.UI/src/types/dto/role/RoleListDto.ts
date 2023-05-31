import { RoleListItemDto } from "./RoleListItemDto";

export interface RoleListDto {
    nonDiveCertificateRoles: RoleListItemDto[];
    diveCertificateRoles: RoleListItemDto[];
}