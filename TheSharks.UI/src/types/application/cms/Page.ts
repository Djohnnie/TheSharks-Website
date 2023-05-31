import { CmsComponent } from "./CmsComponent";

export interface Page {
    id: string;
    title: string;
    link: string;
    isOnlyAvailableForMembers: boolean;
    isDefaultPage: boolean;
    isDefaultPageForMembers: boolean;
    navBarPosition: number;
    navBarSubPosition: number;
    components: CmsComponent[]
}