import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { RoleService } from 'src/app/services/role/role.service';
import { snackbarConfig } from 'src/config/SnackbarConfig';

interface DialogData {
    name: string;
    id: string;
}

@Component({
    selector: 'app-delete-role-dialog',
    templateUrl: './delete-role-dialog.component.html',
    styleUrls: ['./delete-role-dialog.component.scss']
})
export class DeleteRoleDialogComponent implements OnInit {

    constructor(
        @Inject(MAT_DIALOG_DATA) public data: DialogData,
        private _rs: RoleService,
        private _sb: MatSnackBar,
        private _router: Router,
        public ref: MatDialogRef<DeleteRoleDialogComponent>
    ) { }

    ngOnInit(): void {
    }

    async onDelete() {
        const response = await this._rs.deleteRole(this.data.id)

        if (response.error === undefined) {
            this._sb.open("Rol verwijderd", "", snackbarConfig("success"))
            this._router.navigateByUrl("/roles")
            this.ref.close()
        } else {
            this._sb.open(response.error.message, "", snackbarConfig("error"))
        }
    }
}
