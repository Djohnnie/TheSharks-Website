import { CmsComponent } from "src/types/application/cms/CmsComponent";

export interface UpdatePageDto {
    id: string;
    title: string;
    link: string;
    isOnlyAvailableForMembers: boolean;
    isDefaultPage: boolean;
    isDefaultPageForMembers: boolean;
    components: CmsComponent[]
    navBarPosition: number;
    navBarSubPosition: number;
}