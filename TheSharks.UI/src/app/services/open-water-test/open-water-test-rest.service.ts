import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { OpenWaterTestsOverviewListDto } from 'src/types/dto/open-water-test/OpenWaterTestsOverviewListDto';
import { OpenWaterTestDto } from 'src/types/dto/open-water-test/OpenWaterTestDto';
import { AddOpenWaterTestDto } from 'src/types/dto/open-water-test/AddOpenWaterTestDto';
import { UpdateOpenWaterTestDto } from 'src/types/dto/open-water-test/UpdateOpenWaterTestDto';

@Injectable({
    providedIn: 'root'
})
export class OpenWaterTestRestService {

    constructor(private _http: HttpClient) { }

    getAll(diveCertificate: string) {
        return this._http.get<OpenWaterTestsOverviewListDto>(`${environment.baseUrl}/api/openwatertest?diveCertificate=${diveCertificate}`)
    }

    get(id: string) {
        return this._http.get<OpenWaterTestDto>(`${environment.baseUrl}/api/openwatertest/${id}`)
    }

    post(dto: AddOpenWaterTestDto) {
        return this._http.post(`${environment.baseUrl}/api/openwatertest`, dto)
    }

    put(dto: UpdateOpenWaterTestDto) {
        return this._http.put(`${environment.baseUrl}/api/openwatertest/${dto.id}`, dto)
    }

    delete(id: string) {
        return this._http.delete(`${environment.baseUrl}/api/openwatertest/${id}`)
    }
}