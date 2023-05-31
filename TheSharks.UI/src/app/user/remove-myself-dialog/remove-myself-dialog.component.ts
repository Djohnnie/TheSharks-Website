import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { MemberService } from 'src/app/services/member/member.service';
import { snackbarConfig } from 'src/config/SnackbarConfig';

@Component({
    selector: 'remove-myself-dialog',
    templateUrl: './remove-myself-dialog.component.html',
    styleUrls: ['./remove-myself-dialog.component.scss']
})
export class RemoveMyselfDialogComponent implements OnInit {

    constructor(
        @Inject(MAT_DIALOG_DATA) public data: string,
        private _ms: MemberService,
        private _sb: MatSnackBar,
        private _router: Router,
        public ref: MatDialogRef<RemoveMyselfDialogComponent>
    ) { }

    ngOnInit(): void {
    }

    async onDelete() {
        const response = await this._ms.deleteMyself(this.data)

        if (response.error === undefined) {
            this._sb.open("Je account is succesvol verwijderd en je bent automatisch afgemeld", "", snackbarConfig("success"))
            this.ref.close(true)
        } else {
            this._sb.open(response.error.message, "", snackbarConfig("error"))
            this.ref.close(false)
        }
    }
}