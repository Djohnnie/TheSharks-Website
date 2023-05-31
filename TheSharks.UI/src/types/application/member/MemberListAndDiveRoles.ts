import { MemberListItem } from "./MemberListItem";

export interface MemberListAndDiveRoles {
    members: MemberListItem[];
    diveRoles: {
        name: string;
        id: string;
    }[]
}