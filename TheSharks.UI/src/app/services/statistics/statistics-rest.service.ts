import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { StatisticsOverviewListDto } from 'src/types/dto/statistics/StatisticsOverviewListDto';

@Injectable({
    providedIn: 'root'
})
export class StatisticsRestService {

    constructor(private _http: HttpClient) {

    }

    getAllStatistics() {
        return this._http.get<StatisticsOverviewListDto>(`${environment.baseUrl}/api/Statistics`);
    }
}