import { Component, OnInit } from '@angular/core';
import { UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { UserService } from 'src/app/services/user/user.service';
import { snackbarConfig } from 'src/config/SnackbarConfig';

@Component({
    selector: 'app-register',
    templateUrl: './register.component.html',
    styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {

    registerForm = new UntypedFormGroup({
        firstName: new UntypedFormControl("", [Validators.required]),
        lastName: new UntypedFormControl("", [Validators.required]),
        userName: new UntypedFormControl("", [Validators.required]),
        email: new UntypedFormControl("", [Validators.required, Validators.email])
    })

    errorText = "";
    feedbackText = "";
    loading = false;

    constructor(
        private _us: UserService,
        private _sb: MatSnackBar
    ) { }

    ngOnInit(): void {
    }

    async onSubmit() {
        this.loading = true;
        const values = this.registerForm.value

        const response = await this._us.registerUser(
            values.firstName,
            values.lastName,
            values.userName,
            values.email
        )

        if (!response.error) {
            this.loading = false
            this._sb.open("Lid is aangemaakt", "", snackbarConfig("success"))
        } else {
            this.loading = false;
            this._sb.open(response.error.message, "", snackbarConfig("error"))
        }
    }
}
