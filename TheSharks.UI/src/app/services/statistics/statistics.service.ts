import { Injectable } from '@angular/core';
import { StatisticsOverviewListDto } from 'src/types/dto/statistics/StatisticsOverviewListDto';
import { StatisticsRestService } from './statistics-rest.service';
import { response } from '../response';

@Injectable({
    providedIn: 'root'
})
export class StatisticsService {

    constructor(private _rest: StatisticsRestService) { }

    async getAllStatistics() {
        const o$ = this._rest.getAllStatistics()
        return response<StatisticsOverviewListDto>(o$)
    }
}