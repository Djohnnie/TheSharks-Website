import { Injectable } from '@angular/core';
import { GalleryDto } from 'src/types/dto/gallery/GalleryDto';
import { GalleryRestService } from './gallery-rest.service';
import { AddGalleryPicturesDto } from 'src/types/dto/gallery/AddGalleryPictureDto';
import { RemoveGalleryPicturesDto } from 'src/types/dto/gallery/RemoveGalleryPicturesDto';
import { RemovePictureModel } from 'src/types/dto/gallery/RemovePictureModel';
import { response } from '../response';
import { GalleryList } from 'src/types/application/gallery/GalleryList';
import { GalleryDetail } from 'src/types/application/gallery/GalleryDetail';
import { BaseResponse } from 'src/types/BaseResponse';

@Injectable({
    providedIn: 'root'
})

export class GalleryService {

    constructor(private _rest: GalleryRestService) { }

    async addGallery(name: string) {
        const dto: GalleryDto = {
            name: name
        }

        const o$ = this._rest.postGallery(dto)
        return response(o$)
    }

    async getGalleries(page: number, pageSize: number) {
        const o$ = this._rest.getGalleriesByPagination(page, pageSize)
        return response<GalleryList>(o$)
    }

    async getGallery(id: string) {
        const o$ = this._rest.getGallery(id)
        return response<GalleryDetail>(o$)
    }

    async removeGallery(id: string) {
        const o$ = this._rest.deleteGallery(id)
        return response(o$)
    }

    async addPicturesToGallery(id: string, files: File[]) {
        const dto: AddGalleryPicturesDto = {
            id: id,
            pictures: files
        }

        const o$ = this._rest.addPicturesToGallery(dto)
        return response(o$)
    }

    async removePicturesFromGallery(id: string, pictures: RemovePictureModel[]): Promise<BaseResponse<any>> {
        const dto: RemoveGalleryPicturesDto = {
            id: id,
            pictures: pictures
        }

        const o$ = this._rest.deletePicturesFromGallery(dto)
        return response(o$)
    }
}