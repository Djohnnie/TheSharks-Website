import { Component, OnInit } from '@angular/core';
import { UntypedFormControl, UntypedFormGroup } from '@angular/forms';
import { Breakpoints, BreakpointObserver } from '@angular/cdk/layout';
import { MatSnackBar } from '@angular/material/snack-bar';
import { firstValueFrom, map, Observable, shareReplay } from 'rxjs';
import { UserService } from 'src/app/services/user/user.service';
import { snackbarConfig } from 'src/config/SnackbarConfig';
import { User } from 'src/types/application/user/User';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth/auth.service';
import { MatDialog } from '@angular/material/dialog';
import { RemoveMyselfDialogComponent } from '../remove-myself-dialog/remove-myself-dialog.component';
import { size_10MB } from 'src/util/constants';

@Component({
    selector: 'app-edit-profile',
    templateUrl: './edit-profile.component.html',
    styleUrls: ['./edit-profile.component.scss']
})
export class EditProfileComponent implements OnInit {
    user: User | undefined
    ready = false

    editProfileForm = new UntypedFormGroup({
        picture: new UntypedFormControl(null),
        userName: new UntypedFormControl(""),
        firstName: new UntypedFormControl(""),
        lastName: new UntypedFormControl(""),
        phoneNumber: new UntypedFormControl(""),
        bio: new UntypedFormControl("")
    })

    isHandset$: Observable<boolean> = this._bo.observe([Breakpoints.Small, Breakpoints.XSmall])
        .pipe(
            map(result => result.matches),
            shareReplay()
        );

    imagePreview = "/assets/person-placeholder.jpg";

    constructor(
        private _us: UserService, 
        private _as: AuthService,
        private _router: Router,
        private _sb: MatSnackBar,
        private _bo: BreakpointObserver,        
        public dialog: MatDialog) {
    }

    async ngOnInit() {
        this.editProfileForm.disable()

        const temp = await firstValueFrom(this._us.currentUser$)

        if (temp) {
            this.user = temp
        } else {
            this.user = (await this._us.getUser()).response!
        }

        //if user found
        if (this.user !== undefined) {
            this.ready = true
            //If user has picutre
            if (this.user?.profilePicture !== null) {
                this.imagePreview = "data:image/png;base64," + this.user.profilePicture
            }

            //Load user
            this.editProfileForm.get("userName")?.setValue(this.user.userName);
            this.editProfileForm.get("firstName")?.setValue(this.user.firstName);
            this.editProfileForm.get("lastName")?.setValue(this.user.lastName);
            this.editProfileForm.get("phoneNumber")?.setValue(this.user.phoneNumber ?? "");
            this.editProfileForm.get("bio")?.setValue(this.user.bio ?? "");
        }
    }

    onToggleEditMode() {
        if (this.editProfileForm.disabled) {
            this.editProfileForm.enable()
        } else {
            this.editProfileForm.disable()
            //Change picture back to previous
            if (this.user !== undefined) {
                if (this.user?.profilePicture !== null) {
                    this.imagePreview = "data:image/png;base64," + this.user.profilePicture;
                } else {
                    this.imagePreview = "/assets/person-placeholder.jpg";
                }
            }
        }

        this.editProfileForm.controls["userName"].disable()
        this.editProfileForm.controls["firstName"].disable()
        this.editProfileForm.controls["lastName"].disable()
    }

    async onSaveProfile() {
        if (this.user === undefined) {
            return;
        }

        this.ready = false

        //Persist changes
        const response = await this._us.editUser(
            this.user.id,
            this.editProfileForm.value.phoneNumber ?? "",
            this.editProfileForm.value.bio ?? "",
            this.editProfileForm.value.picture
        )

        if (response.error === undefined) {
            this._sb.open("Profiel bewerkt", "", snackbarConfig("success"))
            //Refetch user
            this.user = (await this._us.forceGetUser()).response
            this.onToggleEditMode()
        } else {
            this._sb.open(response.error.message, "", snackbarConfig("error"))
        }
        
        this.ready = true;
    }

    onImageSelect(fileUploader: HTMLInputElement) {

        //Preview image and save change to form
        if (fileUploader.files?.length !== undefined) {
            if (fileUploader.files?.length > 0) {
                const file = fileUploader.files[0];

                if (file.size > size_10MB) {
                    this._sb.open("Het gekozen bestand is te groot", "", snackbarConfig("error"))
                } else {
                    this.editProfileForm.patchValue({ picture: file });
                    this.editProfileForm.updateValueAndValidity();

                    const reader = new FileReader();
                    reader.onload = () => {
                        this.imagePreview = reader.result!.toString();
                    }

                    reader.readAsDataURL(file)
                }
            }
        }
    }

    async removeMyself() {
        const ref = this.dialog.open(RemoveMyselfDialogComponent, { data: this.user?.id })

        if (await firstValueFrom(ref.afterClosed())) {
            this._as.logout();
            this._router.navigateByUrl("/")
        }
    }
}