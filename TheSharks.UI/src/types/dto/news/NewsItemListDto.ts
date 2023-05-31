import { NewsItemDto } from "./NewsItemDto";

export interface NewsItemListDto {
    totalRecords: number;
    newsItems: NewsItemDto[]
}