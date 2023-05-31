import { Component, OnInit } from '@angular/core';
import { UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { firstValueFrom } from 'rxjs';
import { ActivityService } from 'src/app/services/activity/activity.service';
import { snackbarConfig } from 'src/config/SnackbarConfig';
import { Activity } from 'src/types/application/activity/Activity';

@Component({
    selector: 'app-edit-activity',
    templateUrl: './edit-activity.component.html',
    styleUrls: ['./edit-activity.component.scss']
})
export class EditActivityComponent implements OnInit {

    type = ""
    id = ""
    loadingCall = false
    minDate: Date;

    editActivityForm = new UntypedFormGroup({
        title: new UntypedFormControl(null, [Validators.required]),
        location: new UntypedFormControl(null, [Validators.required]),
        locationLink: new UntypedFormControl(null),
        info: new UntypedFormControl(null),
        memberInfo: new UntypedFormControl(null),
        date: new UntypedFormControl(null, [Validators.required]),
        departure: new UntypedFormControl(null),
        briefingTime: new UntypedFormControl(null),
        tide: new UntypedFormControl(null),
        atWater: new UntypedFormControl(null),
        startTime: new UntypedFormControl(null),
        endTime: new UntypedFormControl(null)
    })

    activity: Activity | undefined

    constructor(
        private _route: ActivatedRoute,
        private _as: ActivityService,
        private _sb: MatSnackBar,
        private _router: Router
    ) {
        const now = new Date()
        this.minDate = new Date(now.getFullYear(), now.getMonth(), now.getDate())
    }

    async ngOnInit() {
        const params = await firstValueFrom(this._route.params)
        this.id = params["id"]
        this.type = params["type"]

        await this.loadActivityValues()
    }

    private async loadActivityValues() {
        this.activity = (await this._as.getActivity(this.id, this.type)).response
        for (let prop in this.activity) {
            const value = this.activity[prop]
            const control = this.editActivityForm.get(prop)
            if (value instanceof Date) {
                if (prop === "date") control?.setValue(value)
                else control?.setValue(value.toLocaleTimeString("nl-BE"))
            } else {
                control?.setValue(value)
            }
        }
    }

    private convertToDate(date: Date, formTimeString: string) {
        const hours = +formTimeString.split(":")[0]
        const minutes = +formTimeString.split(":")[1]
        return new Date(date.getFullYear(), date.getMonth(), date.getDate(), hours, minutes)
    }

    async onSave() {
        if (!this.editActivityForm.valid) return;
        this.loadingCall = true

        const values = this.editActivityForm.value
        const d = this.activity!.date

        const response = await this._as.updateActivity(this.id, {
            title: values.title,
            info: values.info,
            location: values.location,
            locationLink: values.locationLink,
            memberInfo: values.memberInfo,
            date: values.date,
            atWater: values.atWater ? this.convertToDate(d, values.atWater) : undefined,
            briefingTime: values.briefingTime ? this.convertToDate(d, values.briefingTime) : undefined,
            departure: values.departure ? this.convertToDate(d, values.departure) : undefined,
            endTime: values.endTime ? this.convertToDate(d, values.endTime) : undefined,
            startTime: values.startTime ? this.convertToDate(d, values.startTime) : undefined,
            tide: values.tide,
            type: this.type,
            id: this.id
        })

        if (response.error === undefined) {
            this._sb.open("Activiteit gewijzigd", "", snackbarConfig("success"))
            this._router.navigateByUrl("/activities/" + this.type + "/" + this.id)
        } else {
            this._sb.open(response.error.message, "", snackbarConfig("error"))
        }

        this.loadingCall = false
    }
}
