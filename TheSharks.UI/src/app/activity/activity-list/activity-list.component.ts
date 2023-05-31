import { Component, OnInit, ViewChild } from '@angular/core';
import { Breakpoints, BreakpointObserver } from '@angular/cdk/layout';
import { firstValueFrom, map, Observable, shareReplay, tap } from 'rxjs';
import { UntypedFormControl, UntypedFormGroup } from '@angular/forms';
import { ActivityService } from 'src/app/services/activity/activity.service';
import { default as _rollupMoment, Moment } from 'moment';
import { DateAdapter, MAT_DATE_FORMATS, MAT_DATE_LOCALE } from '@angular/material/core';
import { MomentDateAdapter, MAT_MOMENT_DATE_ADAPTER_OPTIONS } from '@angular/material-moment-adapter';
import { MatDatepicker } from '@angular/material/datepicker';
import moment from 'moment';
import { ActivityList } from 'src/types/application/activity/ActivityList';
import { ActivityListItem } from 'src/types/application/activity/ActivityListItem';
import { AuthService } from 'src/app/services/auth/auth.service';
import { Claims } from 'src/types/application/role/Claims';

const MY_FORMATS = {
    parse: {
        dateInput: 'MM/YYYY',
    },
    display: {
        dateInput: 'MM/YYYY',
        monthYearLabel: 'MMM YYYY',
        dateA11yLabel: 'LL',
        monthYearA11yLabel: 'MMMM YYYY',
    },
}

@Component({
    selector: 'app-activity-list',
    templateUrl: './activity-list.component.html',
    styleUrls: ['./activity-list.component.scss'],
    providers: [
        {
            provide: DateAdapter,
            useClass: MomentDateAdapter,
            deps: [MAT_DATE_LOCALE, MAT_MOMENT_DATE_ADAPTER_OPTIONS],
        },
        {
            provide: MAT_DATE_FORMATS,
            useValue: MY_FORMATS
        }
    ]
})
export class ActivityListComponent implements OnInit {
    activityList: ActivityList | undefined
    pageSize = 10

    filterForm = new UntypedFormGroup({
        month: new UntypedFormControl(),
        type: new UntypedFormControl("")
    })
    
    authorized = false

    isHandset$: Observable<boolean> = this._bo.observe([Breakpoints.Small, Breakpoints.XSmall])
        .pipe(
            map(result => result.matches),
            shareReplay()
        );

    constructor(
        private _as: ActivityService, 
        private _auth: AuthService,        
        private _bo: BreakpointObserver) {
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
        this.activityList = (await this._as.getActivities(p, this.pageSize, this.filterForm.value.month, this.filterForm.value.type)).response
    }

    onClearFilters() {
        this.filterForm.reset({ month: null, type: "" })
        this.onFilter()
    }

    setMonthAndYear(normalizedMonthAndYear: Moment, datepicker: MatDatepicker<Moment>) {
        const control = this.filterForm.get("month")
        control?.setValue(moment())
        const ctrlValue = control?.value
        ctrlValue.month(normalizedMonthAndYear.month());
        ctrlValue.year(normalizedMonthAndYear.year());
        this.filterForm.get("month")?.setValue(ctrlValue);
        datepicker.close();
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