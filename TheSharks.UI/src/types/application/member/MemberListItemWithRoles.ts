import { RoleListItem } from "../role/RoleListItem";
import { MemberListItem } from "./MemberListItem";

export interface MemberListItemWithRoles extends MemberListItem {
    roles: {
        concernsDivingCertificate: boolean;
        name: string;
        id: string;
    }[]
}