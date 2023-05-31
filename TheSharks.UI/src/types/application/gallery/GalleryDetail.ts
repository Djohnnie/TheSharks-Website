import { GalleryPicture } from "./GalleryPicture";

export interface GalleryDetail {
    id: string;
    name: string;
    pictures: GalleryPicture[];
}