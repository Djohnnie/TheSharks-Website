import { Component, OnInit } from '@angular/core';
import { AbstractControl, UntypedFormControl, UntypedFormGroup, ValidationErrors, ValidatorFn, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { firstValueFrom } from 'rxjs';
import { UserService } from 'src/app/services/user/user.service';

@Component({
    selector: 'app-reset-password',
    templateUrl: './reset-password.component.html',
    styleUrls: ['./reset-password.component.scss']
})
export class ResetPasswordComponent implements OnInit {

    passwordMatchValidator: ValidatorFn = (control: AbstractControl): ValidationErrors | null => {
        const p = control.get("password")
        const cp = control.get("confirmPassword")

        return p?.value === cp?.value ? null : { passwordUnmatched: true }
    }

    passwordPatternValidator: ValidatorFn = (control: AbstractControl): ValidationErrors | null => {
        if (!control.value) {
            return null
        }
        const regex = new RegExp(/^(?=.*\d)(?=.*[a-z]).{8,}$/);
        const valid = regex.test(control.value);
        return valid ? null : { invalidPassword: true };
    }

    resetPasswordForm = new UntypedFormGroup({
        password: new UntypedFormControl("", [Validators.required, this.passwordPatternValidator]),
        confirmPassword: new UntypedFormControl("", [Validators.required])
    }, {
        validators: [this.passwordMatchValidator]
    })

    private _id = "";
    private _token = "";
    loading = false;
    showPassword = false
    valid = false;

    constructor(
        private _route: ActivatedRoute,
        private _us: UserService,
        private _router: Router
    ) {

    }

    async ngOnInit() {
        const params = await firstValueFrom(this._route.queryParams)
        this._id = params["Id"]
        this._token = params["token"]

        this.valid = this._id != null && this._token != null
    }

    async onSubmit() {
        this.loading = true;
        if (await this._us.resetPassword(
            this._id,
            this._token,
            this.resetPasswordForm.value.password
        )) {
            this._router.navigateByUrl("/login")
        }
    }

    togglePassword() {
        this.showPassword = !this.showPassword
    }
}