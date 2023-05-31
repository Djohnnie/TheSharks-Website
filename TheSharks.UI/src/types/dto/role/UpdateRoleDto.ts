import { ClaimDto } from "./ClaimDto";

export interface UpdateRoleDto {
    id: string;
    claims: ClaimDto[]
}