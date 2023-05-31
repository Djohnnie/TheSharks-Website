import { Component, OnInit } from '@angular/core';
import { UntypedFormControl, UntypedFormGroup } from '@angular/forms';
import { ActivityService } from 'src/app/services/activity/activity.service';
import { ActivityList } from 'src/types/application/activity/ActivityList';
import { ActivityListItem } from 'src/types/application/activity/ActivityListItem';
import { AuthService } from 'src/app/services/auth/auth.service';
import { Claims } from 'src/types/application/role/Claims';
import moment from 'moment';

@Component({
    selector: 'app-activity-light',
    templateUrl: './activity-light.component.html',
    styleUrls: ['./activity-light.component.scss']
})
export class ActivityLightComponent implements OnInit {
    activityList: ActivityList | undefined
    pageSize = 10

    filterForm = new UntypedFormGroup({
        month: new UntypedFormControl(),
        type: new UntypedFormControl("")
    })

    authorized = false

    constructor(private _as: ActivityService, private _auth: AuthService) {
    }

    async ngOnInit() {
        this.loadActivities()
        this.authorized = this._auth.authorizeClaims([Claims.ManageActivities])
    }

    async onPageChanged(event: number) {
        this.loadActivities(event)
    }

    async onFilter() {
        this.loadActivities()
    }

    async loadActivities(page?: number) {
        let p = 1
        if (page) p = page
        this.activityList = (await this._as.getActivities(p, this.pageSize)).response
    }

    isActivityInFuture(activity: ActivityListItem) : boolean {
        const utcMoment = moment.utc();
        const utcDate = new Date( utcMoment.format() );

        const activityDate = new Date(activity.date);
        activityDate.setHours(0,0,0,0);

        if( activity.startTime )
        {
            activityDate.setHours(activity.startTime.getHours(),activity.startTime.getMinutes(),0,0);
        }

        if( activity.departure )
        {
            activityDate.setHours(activity.departure.getHours(),activity.departure.getMinutes(),0,0);
        }

        if( !activity.departure && activity.briefingTime )
        {
            activityDate.setHours(activity.briefingTime.getHours(),activity.briefingTime.getMinutes(),0,0);
        }

        if( !activity.departure && !activity.briefingTime && activity.atWater )
        {
            activityDate.setHours(activity.atWater.getHours(),activity.atWater.getMinutes(),0,0);
        }
        
        return moment(activityDate).isSameOrAfter(utcDate)
    }
}