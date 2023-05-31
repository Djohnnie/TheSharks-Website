import { Injectable } from '@angular/core';
import { StatusRestService } from './status-rest.service';
import { response } from '../response';

@Injectable({
    providedIn: 'root'
})
export class StatusService {
    constructor(private _rest: StatusRestService) { }

    async getVersion() {
        const o$ = this._rest.getVersion()
        return response(o$)
    }
}