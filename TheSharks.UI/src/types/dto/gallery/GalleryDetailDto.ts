import { GalleryPictureDto } from "./GalleryPictureDto";

export interface GalleryDetailDto {
    id: string;
    name: string;
    pictures: GalleryPictureDto[];
}