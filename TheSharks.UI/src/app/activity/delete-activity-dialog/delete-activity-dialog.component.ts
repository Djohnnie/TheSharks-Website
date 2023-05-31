import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { ActivityService } from 'src/app/services/activity/activity.service';
import { snackbarConfig } from 'src/config/SnackbarConfig';

@Component({
    selector: 'app-delete-activity-dialog',
    templateUrl: './delete-activity-dialog.component.html',
    styleUrls: ['./delete-activity-dialog.component.scss']
})
export class DeleteActivityDialogComponent implements OnInit {

    constructor(
        @Inject(MAT_DIALOG_DATA) public data: string,
        private _as: ActivityService,
        private _sb: MatSnackBar,
        private _router: Router,
        public ref: MatDialogRef<DeleteActivityDialogComponent>
    ) { }

    ngOnInit(): void {
    }

    async onDelete() {
        const response = await this._as.deleteActivity(this.data)

        if (response.error === undefined) {
            this._sb.open("Activiteit verwijderd", "", snackbarConfig("success"))
            this._router.navigateByUrl("/activities")
            this.ref.close()
        } else {
            this._sb.open(response.error.message, "", snackbarConfig("error"))
        }
    }
}
