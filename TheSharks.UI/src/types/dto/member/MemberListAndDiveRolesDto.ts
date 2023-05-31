import { MemberListItem } from "src/types/application/member/MemberListItem";
import { RoleListItemDto } from "../role/RoleListItemDto";

export interface MemberListAndDiveRolesDto {
    members: MemberListItem[];
    diveRoles: RoleListItemDto[]
}