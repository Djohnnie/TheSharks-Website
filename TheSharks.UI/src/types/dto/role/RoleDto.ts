import { ClaimDto } from "src/types/dto/role/ClaimDto"

export interface RoleDto {
    claims: ClaimDto[]
    id: string;
    name: string;
}