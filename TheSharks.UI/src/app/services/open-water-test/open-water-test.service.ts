import { Injectable } from '@angular/core';
import { OpenWaterTestsOverviewListDto } from 'src/types/dto/open-water-test/OpenWaterTestsOverviewListDto';
import { OpenWaterTestDto } from 'src/types/dto/open-water-test/OpenWaterTestDto';
import { OpenWaterTestRestService } from './open-water-test-rest.service';
import { response } from '../response';

@Injectable({
    providedIn: 'root'
})
export class OpenWaterTestService {

    constructor(private _rest: OpenWaterTestRestService) { }

    async getAllOpenWaterTests(diveCertificate: string) {
        const o$ = this._rest.getAll(diveCertificate)
        return response<OpenWaterTestsOverviewListDto>(o$)
    }

    async getOpenWaterTest(id: string) {
        const o$ = this._rest.get(id)
        return response<OpenWaterTestDto>(o$)
    }

    async addOpenWaterTest(title: string, diveCertificate: string, content: string) {
        const o$ = this._rest.post({
            title: title,
            diveCertificate: diveCertificate,
            content: content
        })

        return response(o$)
    }

    async updateOpenWaterTest(id: string, title: string, diveCertificate: string, content: string) {
        const o$ = this._rest.put({
            id: id,
            title: title,
            diveCertificate: diveCertificate,
            content: content
        })

        return response(o$)
    }

    async removeOpenWaterTest(id: string) {
        const o$ = this._rest.delete(id)
        return response(o$)
    }
}