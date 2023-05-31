import { ActivityListItemDto } from "./ActivityListItemDto";

export interface ActivityListDto {
    totalRecords: number;
    activities: ActivityListItemDto[]
}