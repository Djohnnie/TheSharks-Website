import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { EnrollmentService } from 'src/app/services/enrollment/enrollment.service';
import { snackbarConfig } from 'src/config/SnackbarConfig';

interface DialogData {
    memberId: string;
    activityId: string;
}

@Component({
    selector: 'app-delete-enrollment-dialog',
    templateUrl: './delete-enrollment-dialog.component.html',
    styleUrls: ['./delete-enrollment-dialog.component.scss']
})
export class DeleteEnrollmentDialogComponent implements OnInit {

    constructor(
        @Inject(MAT_DIALOG_DATA) public data: DialogData,
        private _es: EnrollmentService,
        private _sb: MatSnackBar,
        private _router: Router,
        public ref: MatDialogRef<DeleteEnrollmentDialogComponent>
    ) { }

    ngOnInit(): void {
    }

    async onDelete() {
        const response = await this._es.deleteEnrollment(this.data.memberId, this.data.activityId)

        if (response.error === undefined) {
            this._sb.open("Je bent uitgeschreven", "", snackbarConfig("success"))
            this.ref.close(true)
        } else {
            this._sb.open(response.error.message, "", snackbarConfig("error"))
        }
    }
}
