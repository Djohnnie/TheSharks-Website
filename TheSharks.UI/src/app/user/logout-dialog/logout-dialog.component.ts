import { Component, OnInit } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth/auth.service';

@Component({
    selector: 'app-logout-dialog',
    templateUrl: './logout-dialog.component.html',
    styleUrls: ['./logout-dialog.component.scss']
})
export class LogoutDialogComponent implements OnInit {

    constructor(
        private dialogRef: MatDialogRef<LogoutDialogComponent>,
        private _as: AuthService,
        private _router: Router
    ) { }

    ngOnInit(): void {
    }

    onLogout() {
        this.dialogRef.close()
        this._as.logout();
        this._router.navigateByUrl("/login")
    }
}
