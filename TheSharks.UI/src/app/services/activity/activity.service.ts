import { Injectable } from '@angular/core';
import { AddActivityDto } from 'src/types/dto/AddActivityDto';
import { Activity } from 'src/types/application/activity/Activity';
import { BaseResponse } from 'src/types/BaseResponse';
import { ActivityRestService } from './activity-rest.service';
import { ActivityList } from 'src/types/application/activity/ActivityList';
import { Moment } from 'moment';
import { ActivityListItem } from 'src/types/application/activity/ActivityListItem';
import { UpdateActivityDto } from 'src/types/dto/activity/UpdateActivityDto';
import { response } from '../response';

@Injectable({
    providedIn: 'root'
})
export class ActivityService {
    constructor(private _rest: ActivityRestService) { }

    getActivities$(page: number, size: number) {
        return this._rest.getActivitiesByPagination(page, size)
    }

    async getActivities(page: number, size: number, dateFilter?: Moment, typeFilter?: string) {
        const o$ = this._rest.getActivitiesByPagination(page, size, dateFilter?.toDate(), typeFilter)
        return response<ActivityList>(o$)
    }

    async getActivity(id: string, type: string): Promise<BaseResponse<Activity>> {
        const o$ = this._rest.getActivity(id, type)
        return response<Activity>(o$)
    }

    async getUpcomingActivity(userId: string) {
        const o$ = this._rest.getUpcomingActivity(userId)
        return response<ActivityListItem | null>(o$)
    }

    async addActivity(activity: AddActivityDto) {
        const o$ = this._rest.postActivity(activity)
        return response<any>(o$)
    }

    async updateActivity(id: string, activity: UpdateActivityDto) {
        const o$ = this._rest.putActivity(id, activity)
        return response(o$)
    }

    async deleteActivity(id: string) {
        const o$ = this._rest.deleteActivity(id)
        return response(o$)
    }
}
