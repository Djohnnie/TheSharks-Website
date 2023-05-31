import { GalleryListItemDto } from "./GalleryListItemDto";

export interface GalleryListDto{
    totalRecords: number;
    galleries: GalleryListItemDto[]
}