import { BreakpointObserver } from '@angular/cdk/layout';
import { StepperOrientation, StepperSelectionEvent } from '@angular/cdk/stepper';
import { Component, OnInit } from '@angular/core';
import { UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { firstValueFrom, map, Observable } from 'rxjs';
import { ActivityService } from 'src/app/services/activity/activity.service';
import { MemberService } from 'src/app/services/member/member.service';
import { snackbarConfig } from 'src/config/SnackbarConfig';
import { Activity } from 'src/types/application/activity/Activity';
import { MemberListItem } from 'src/types/application/member/MemberListItem';
import { MemberListDialogComponent } from '../member-list-dialog/member-list-dialog.component';

@Component({
    selector: 'app-add-activity',
    templateUrl: './add-activity.component.html',
    styleUrls: ['./add-activity.component.scss']
})
export class AddActivityComponent implements OnInit {

    loading = false;
    minDate: Date;
    private _memberList: MemberListItem[] | undefined
    responsibleName: string = ""

    tempActivity: Activity | undefined
    type = new UntypedFormControl("dive", [Validators.required])

    activityForm = new UntypedFormGroup({
        title: new UntypedFormControl("", [Validators.required]),
        date: new UntypedFormControl(null, [Validators.required]),
        location: new UntypedFormControl("", [Validators.required]),
        locationLink: new UntypedFormControl(),
        info: new UntypedFormControl(),
        memberInfo: new UntypedFormControl(),
        necessarySubscription: new UntypedFormControl(false),
        responsible: new UntypedFormControl("", [Validators.required])
    })

    diveForm = new UntypedFormGroup({
        departure: new UntypedFormControl(),
        briefingTime: new UntypedFormControl(),
        tide: new UntypedFormControl(),
        atWater: new UntypedFormControl()
    })

    eventForm = new UntypedFormGroup({
        departure: new UntypedFormControl(),
        startTime: new UntypedFormControl(),
        endTime: new UntypedFormControl()
    })

    meetingForm = new UntypedFormGroup({
        startTime: new UntypedFormControl(),
        endTime: new UntypedFormControl()
    })

    constructor(
        private _as: ActivityService,
        private _sb: MatSnackBar,
        private _router: Router,
        private _ms: MemberService,
        public dialog: MatDialog
    ) {
        const now = new Date()
        this.minDate = new Date(now.getFullYear(), now.getMonth(), now.getDate())
    }

    ngOnInit(): void {
    }

    async onSubmit() {
        this.loading = true

        const response = await this._as.addActivity({
            ...this.tempActivity!,
            responsibleId: this.activityForm.value.responsible
        })

        if (response.error === undefined) {
            this._sb.open("Activiteit aangemaakt", "", snackbarConfig("success"))
            this._router.navigateByUrl(`/activities/${this.tempActivity?.activityType}/${response.response.id}`)
        } else {
            this._sb.open(response.error.message, "", snackbarConfig("error"))
        }

        this.loading = false
    }

    private updateTempActivity() {
        const general = this.activityForm.value;
        const dive = this.diveForm.value;
        const event = this.eventForm.value;
        const meeting = this.meetingForm.value;

        const date: Date = general.date

        //Check for form values and convert to date
        let briefingTime: Date | undefined
        if (dive.briefingTime) {
            briefingTime = this.convertToDate(date, dive.briefingTime)
        }

        let atWater: Date | undefined
        if (dive.atWater) {
            atWater = this.convertToDate(date, dive.atWater)
        }

        let departure: Date | undefined
        if (dive.departure) {
            departure = this.convertToDate(date, dive.departure)
        } else if (event.departure) {
            departure = this.convertToDate(date, event.departure)
        }

        let startTime: Date | undefined
        if (event.startTime) {
            startTime = this.convertToDate(date, event.startTime)
        } else if (meeting.startTime) {
            startTime = this.convertToDate(date, meeting.startTime)
        }

        let endTime: Date | undefined
        if (event.endTime) {
            endTime = this.convertToDate(date, event.endTime)
        } else if (meeting.endTime) {
            endTime = this.convertToDate(date, meeting.endTime)
        }

        let name;
        if (this.type.value === "dive")
            name = "Duik"
        else if (this.type.value === "event")
            name = "Evenement"
        else if (this.type.value === "boardmeeting")
            name = "Bestuursvergadering"
        else
            name = "Monitorsraad"

        let title = general.title;

        this.tempActivity = {
            departure: departure,
            briefingTime: briefingTime,
            tide: dive.tide,
            atWater: atWater,
            startTime: startTime,
            endTime: endTime,
            id: "",
            activityType: this.type.value,
            responsibleId: "",
            responsibleFirstName: "",
            responsibleLastName: "",
            title: title,
            name: name,
            date: date,
            location: general.location,
            locationLink: general.locationLink,
            info: general.info,
            memberInfo: general.memberInfo,
            necessarySubscription: general.necessarySubscription,
            enrollments: []
        }
    }

    private convertToDate(date: Date, formTimeString: string) {
        const hours = +formTimeString.split(":")[0]
        const minutes = +formTimeString.split(":")[1]
        return new Date(date.getFullYear(), date.getMonth(), date.getDate(), hours, minutes)
    }

    onStepChanged(event: StepperSelectionEvent) {
        if (event.selectedIndex === 3) {
            this.updateTempActivity()
        }
    }

    async chooseResponsible() {
        //Don't fetch if present
        if (!this._memberList) {
            this._memberList = (await this._ms.getAllMembers()).response
        }

        const ref = this.dialog.open(MemberListDialogComponent, {
            data: this._memberList
        });

        const result = await firstValueFrom(ref.afterClosed())

        //Check result from dialog
        if (result !== undefined) {
            this.activityForm.get("responsible")?.setValue(result.id)
            this.responsibleName = result.firstName + " " + result.lastName
        }
    }
}