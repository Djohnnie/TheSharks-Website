import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Activity } from 'src/types/application/activity/Activity';
import { AddActivityDto } from 'src/types/dto/AddActivityDto';
import { ActivityDto } from 'src/types/dto/activity/ActivityDto';
import { ActivityListDto } from 'src/types/dto/activity/ActivityListDto';
import { ActivityList } from 'src/types/application/activity/ActivityList';
import { ActivityListItem } from 'src/types/application/activity/ActivityListItem';
import { ActivityListItemDto } from 'src/types/dto/activity/ActivityListItemDto';
import { UpdateActivityDto } from 'src/types/dto/activity/UpdateActivityDto';

@Injectable({
    providedIn: 'root'
})
export class ActivityRestService {

    constructor(private _http: HttpClient) { }

    getActivitiesByPagination(page: number, size: number, dateFilter?: Date, activityTypeFilter?: string) {
        let base = `${environment.baseUrl}/api/Activity?`
        let p = `Page=${page}`
        let r = `RecordsPerPage=${size}`

        let url = base.concat(p).concat("&").concat(r)
        if (dateFilter) url = url.concat("&").concat(`DateFilter=${dateFilter.toISOString()}`)
        if (activityTypeFilter) url = url.concat("&").concat(`ActivityTypeFilter=${activityTypeFilter}`)

        return this._http.get<ActivityListDto>(url).pipe(
            map<ActivityListDto, ActivityList>((v) => {
                return {
                    totalRecords: v.totalRecords, activities: v.activities.map((a) => {
                        return { 
                            ...a, 
                            date: new Date(a.date),
                            startTime: this.convertStringToDate(a.startTime),
                            endTime: this.convertStringToDate(a.endTime),
                            departure: this.convertStringToDate(a.departure),
                            atWater: this.convertStringToDate(a.atWater),
                            briefingTime: this.convertStringToDate(a.briefingTime)
                        }
                    })
                }
            })
        )
    }

    getActivity(id: string, type: string) {
        return this._http.get<ActivityDto>(`${environment.baseUrl}/api/Activity/${type}/${id}`).pipe(
            map<ActivityDto, Activity>((a) => {
                return {
                    ...a,
                    date: new Date(a.date),
                    startTime: this.convertStringToDate(a.startTime),
                    endTime: this.convertStringToDate(a.endTime),
                    departure: this.convertStringToDate(a.departure),
                    atWater: this.convertStringToDate(a.atWater),
                    briefingTime: this.convertStringToDate(a.briefingTime)
                }
            })
        )
    }

    postActivity(activity: AddActivityDto) {
        return this._http.post(`${environment.baseUrl}/api/Activity/${activity.activityType}`, activity)
    }

    getUpcomingActivity(userId: string) {
        return this._http.get<ActivityListItemDto | null>(`${environment.baseUrl}/api/Activity/upcoming?Id=${userId}`).pipe(
            map<ActivityListItemDto | null, ActivityListItem | null>((a) => {
                if (a === null) return null;

                return {
                    ...a,
                    date: new Date(a.date),
                    startTime: this.convertStringToDate(a.startTime),
                    endTime: this.convertStringToDate(a.endTime),
                    departure: this.convertStringToDate(a.departure),
                    atWater: this.convertStringToDate(a.atWater),
                    briefingTime: this.convertStringToDate(a.briefingTime)
                }
            })
        )
    }

    putActivity(id: string, dto: UpdateActivityDto) {
        return this._http.put(`${environment.baseUrl}/api/Activity/${dto.type}/${id}`, dto)
    }

    deleteActivity(id: string) {
        return this._http.delete(`${environment.baseUrl}/api/Activity/${id}`)
    }

    private convertStringToDate(stringDate: string | undefined | null) {
        let date: Date;

        if (stringDate !== null && stringDate !== undefined) {
            date = new Date(stringDate)
            return date
        } else {
            return undefined
        }
    }
}
