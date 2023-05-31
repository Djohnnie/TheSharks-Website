import { NewsItem } from "./NewsItem";

export interface NewsItemList {
    totalRecords: number;
    newsItems: NewsItem[]
}