import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { MemberService } from 'src/app/services/member/member.service';
import { snackbarConfig } from 'src/config/SnackbarConfig';

@Component({
    selector: 'delete-member-dialog',
    templateUrl: './delete-member-dialog.component.html',
    styleUrls: ['./delete-member-dialog.component.scss']
})
export class DeleteMemberDialogComponent implements OnInit {

    constructor(
        @Inject(MAT_DIALOG_DATA) public data: string,
        private _ms: MemberService,
        private _sb: MatSnackBar,
        private _router: Router,
        public ref: MatDialogRef<DeleteMemberDialogComponent>
    ) { }

    ngOnInit(): void {
    }

    async onDelete() {
        const response = await this._ms.deleteMember(this.data)

        if (response.error === undefined) {
            this._sb.open("Lid verwijderd", "", snackbarConfig("success"))
            this.ref.close(true)
        } else {
            this._sb.open(response.error.message, "", snackbarConfig("error"))
        }
    }
}