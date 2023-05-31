import { ClaimDto } from "./ClaimDto";

export interface AddRoleDto {
    name: string;
    concernsDivingCertificate: boolean;
    claims: ClaimDto[]
}