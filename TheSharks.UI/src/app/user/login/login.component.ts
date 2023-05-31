import { Component, OnDestroy, OnInit } from '@angular/core';
import { UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { AuthService } from 'src/app/services/auth/auth.service';
import { CmsService } from 'src/app/services/cms/cms.service';
import { snackbarConfig } from 'src/config/SnackbarConfig';

@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit, OnDestroy {
    private _sub: Subscription | null = null
    loading = false;
    showPassword = false

    loginForm = new UntypedFormGroup({
        username: new UntypedFormControl("", [Validators.required]),
        password: new UntypedFormControl("", [Validators.required]),
        rememberMe: new UntypedFormControl(false)
    })

    constructor(
        private _as: AuthService,
        private _cmss: CmsService,
        private _router: Router,
        private _sb: MatSnackBar
    ) { }

    ngOnDestroy(): void {
        if (this._sub !== null) {
            this._sub!.unsubscribe();
        }
    }

    ngOnInit(): void {
    }

    async onSubmit() {
        this.loading = true
        const values = this.loginForm.value

        const response = await this._as.login(
            values.username,
            values.password,
            values.rememberMe
        )

        const memberPage = await this._cmss.getDefaultMembersPage()

        if (response.error === undefined) {
            this.loading = false;
            this._router.navigateByUrl(`/${memberPage.response}`)
            this._sb.open("Je bent nu aangemeld", "", snackbarConfig("success"))
        } else {
            this.loading = false;
            this._sb.open(response.error.message, "", snackbarConfig("error"))
        }
    }

    togglePassword() {
        this.showPassword = !this.showPassword
    }
}