import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { GalleryList } from 'src/types/application/gallery/GalleryList';
import { GalleryDto } from 'src/types/dto/gallery/GalleryDto';
import { GalleryListDto } from 'src/types/dto/gallery/GalleryListDto';
import { map } from 'rxjs';
import { GalleryDetailDto } from 'src/types/dto/gallery/GalleryDetailDto';
import { GalleryDetail } from 'src/types/application/gallery/GalleryDetail';
import { AddGalleryPicturesDto } from 'src/types/dto/gallery/AddGalleryPictureDto';
import { RemoveGalleryPicturesDto } from 'src/types/dto/gallery/RemoveGalleryPicturesDto';

@Injectable({
    providedIn: 'root'
})
export class GalleryRestService {

    constructor(private _http: HttpClient) {

    }

    postGallery(dto: GalleryDto) {
        return this._http.post(`${environment.baseUrl}/api/Gallery`, dto)
    }

    getGalleriesByPagination(page: number, size: number) {
        let base = `${environment.baseUrl}/api/Gallery?`
        let p = `Page=${page}`
        let r = `RecordsPerPage=${size}`
        let url = base.concat(p).concat("&").concat(r)

        return this._http.get<GalleryListDto>(url).pipe(
            map<GalleryListDto, GalleryList>((_) => {
                return {
                    totalRecords: _.totalRecords, galleries: _.galleries
                }
            })
        )
    }

    getGallery(id: string) {
        return this._http.get<GalleryDetailDto>(`${environment.baseUrl}/api/Gallery/${id}`).pipe(
            map<GalleryDetailDto, GalleryDetail>((_) => {
                return {
                    ..._
                }
            })
        )
    }

    deleteGallery(id: string) {
        return this._http.delete(`${environment.baseUrl}/api/Gallery/${id}`)
    }

    addPicturesToGallery(dto: AddGalleryPicturesDto) {
        const fd = new FormData()
        fd.append("id", dto.id)

        dto.pictures.forEach(_ => fd.append("pictures", _))

        return this._http.post(`${environment.baseUrl}/api/Gallery/${dto.id}/pictures`, fd)
    }

    deletePicturesFromGallery(dto: RemoveGalleryPicturesDto) {
        return this._http.delete(`${environment.baseUrl}/api/Gallery/${dto.id}/pictures`, { body: dto })
    }
}
