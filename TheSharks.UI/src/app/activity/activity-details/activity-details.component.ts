import { trigger, state, style, transition, animate } from '@angular/animations';
import { Component, OnInit } from '@angular/core';
import { BreakpointObserver, Breakpoints } from '@angular/cdk/layout';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable, firstValueFrom } from 'rxjs';
import { map, shareReplay } from 'rxjs/operators';
import { ActivityService } from 'src/app/services/activity/activity.service';
import { AuthService } from 'src/app/services/auth/auth.service';
import { EnrollmentService } from 'src/app/services/enrollment/enrollment.service';
import { UserService } from 'src/app/services/user/user.service';
import { Activity } from 'src/types/application/activity/Activity';
import { Participant } from 'src/types/application/activity/Participant';
import { Claims } from 'src/types/application/role/Claims';
import { User } from 'src/types/application/user/User';
import moment from 'moment';
import { DeleteActivityDialogComponent } from '../delete-activity-dialog/delete-activity-dialog.component';
import { DeleteEnrollmentDialogComponent } from '../delete-enrollment-dialog/delete-enrollment-dialog.component';
import { ParticipantDialogComponent } from '../participant-dialog/participant-dialog.component';
import { EmailDialogComponent } from '../../member/email-dialog/email-dialog.component';

@Component({
    selector: 'app-activity-details',
    templateUrl: './activity-details.component.html',
    styleUrls: ['./activity-details.component.scss'],
    animations: [
        trigger('detailExpand', [
            state('collapsed', style({ height: '0px', minHeight: '0' })),
            state('expanded', style({ height: '*' })),
            transition('expanded <=> collapsed', animate('225ms cubic-bezier(0.4, 0.0, 0.2, 1)')),
        ]),
    ],
})

export class ActivityDetailsComponent implements OnInit {

    private _id = ""
    private _type = ""
    activity: Activity | undefined
    isDiveActivity: boolean | undefined
    isResponsible: boolean | undefined
    columns = ["name", "asDiver", "diveCertificate"]
    noDiveColumns = ["name"]
    expandedMember: Participant | undefined

    authorized = false
    userEnrolled = false
    user: User | undefined
    numberOfEnrolledMembers: number = 0
    numberOfMembersWithPhoneNumber: number = 0
    isMobile: boolean = true
    isAndroid: boolean = false
    isIos: boolean = false

    isHandset$: Observable<boolean> = this._bo.observe(Breakpoints.Small)
        .pipe(
            map(result => result.matches),
            shareReplay()
        );

    constructor(
        private _bo: BreakpointObserver,
        private _route: ActivatedRoute,
        private _as: ActivityService,
        private _us: UserService,
        private _auth: AuthService,
        public dialog: MatDialog,
        private _es: EnrollmentService,
        private _router: Router
    ) { }

    async ngOnInit() {
        const params = await firstValueFrom(this._route.params)
        this._id = params["id"]
        this._type = params["type"]
        this.authorized = this._auth.authorizeClaims([Claims.ManageActivities])
        this.loadActivity()
    }

    private async loadActivity() {
        this.isAndroid = this.detectIsAndroid();
        this.isIos = this.detectIsIos();
        this.isMobile = this.isAndroid || this.isIos
        this.user = (await this._us.getUser()).response
        this.activity = (await this._as.getActivity(this._id, this._type)).response
        this.isDiveActivity = this.activity?.activityType === "dive"
        this.isResponsible = this.activity?.responsibleId === this.user?.id
        this.numberOfEnrolledMembers = this.activity?.enrollments.filter(x => x.registreeId).length ?? 0
        this.userEnrolled = Boolean(this.activity?.enrollments.find(e => e.id === this.user!.id))
        let phoneNumbers = this.activity?.enrollments.filter(x => x.registreePhoneNumber).map(x => x.registreePhoneNumber)
        this.numberOfMembersWithPhoneNumber = phoneNumbers?.length ?? 0
    }

    detectIsAndroid() : boolean {
        return /android/i.test(navigator.userAgent);
    }

    detectIsIos() : boolean {
        return /iPad|iPhone|iPod/i.test(navigator.userAgent);
    }

    openParticipantDialog(participant: Participant) {
        this.dialog.open(ParticipantDialogComponent, {
            data: { participant: participant, isDiveActivity: this.isDiveActivity }
        })
    }

    openDeleteDialog() {
        this.dialog.open(DeleteActivityDialogComponent, {
            data: this.activity?.id
        })
    }

    openEmailDialog() {
        this.dialog.open(EmailDialogComponent, {
            width: "80%",
            disableClose: true,
            data: {
                id: this.user?.id,
                selected: this.activity?.enrollments.filter(x => x.registreeId).map(x => x.registreeId),
                areMembers: true,
                subject: this.activity?.title
            }
        })
    }

    openSMS() {
        let phoneNumbers = this.activity?.enrollments.filter(x => x.registreePhoneNumber).map(x => x.registreePhoneNumber)
        
        if( this.isAndroid )
        {
            let link = `sms://` + phoneNumbers?.join(', ')
            window.location.href = link
        }

        if( this.isIos )
        {
            let link = `sms://open?addresses=` + phoneNumbers?.join(',')
            window.location.href = link
        }
    }

    async removeEnrollment() {
        const ref = this.dialog.open(DeleteEnrollmentDialogComponent, {
            data: {
                memberId: this.user!.id,
                activityId: this.activity!.id
            }
        })

        if (await firstValueFrom(ref.afterClosed())) {
            this.loadActivity()
        }
    }

    isActivityInFuture(activity: Activity) : boolean {
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