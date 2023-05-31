import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { map } from 'rxjs';

@Injectable({
    providedIn: 'root'
})
export class StatusRestService {

    constructor(private _http: HttpClient) { }

    getVersion() {
        return this._http.get(`${environment.baseUrl}/api/Status/version`, {responseType: 'text'})
    }
}