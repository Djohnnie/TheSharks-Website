import { Component, OnInit } from '@angular/core';
import { UntypedFormControl, Validators } from '@angular/forms';
import { AuthService } from 'src/app/services/auth/auth.service';

@Component({
    selector: 'app-forgot-password',
    templateUrl: './forgot-password.component.html',
    styleUrls: ['./forgot-password.component.scss']
})
export class ForgotPasswordComponent implements OnInit {

    email = new UntypedFormControl("", [Validators.required, Validators.email])
    feedbackText = ""
    loading = false;

    constructor(private _as: AuthService) { }

    ngOnInit(): void {
    }

    async onSubmit() {
        this.loading = true;
        await this._as.forgotPassword(this.email.value)
        this.loading = false;
        this.feedbackText = "Als je een lid bent, is er een mail verstuurd naar " + this.email.value
    }
}