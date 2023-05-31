import { Claim } from "./Claim";

export interface Role {
    claims: Claim[]
    id: string;
    name: string;
}