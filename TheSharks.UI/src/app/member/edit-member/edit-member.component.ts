import { Component, OnInit } from '@angular/core';
import { UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { firstValueFrom } from 'rxjs';
import { ActivatedRoute, Router } from '@angular/router';
import { MemberService } from 'src/app/services/member/member.service';
import { MemberItem } from 'src/types/application/member/MemberItem';
import { snackbarConfig } from 'src/config/SnackbarConfig';

@Component({
    selector: 'edit-member',
    templateUrl: './edit-member.component.html',
    styleUrls: ['./edit-member.component.scss']
})
export class EditMemberComponent implements OnInit {
    
    private _id = ""
    ready = false
    member: MemberItem | undefined

    editMemberForm = new UntypedFormGroup({
        firstName: new UntypedFormControl("", [Validators.required]),
        lastName: new UntypedFormControl("", [Validators.required]),
        username: new UntypedFormControl("", [Validators.required]),
        email: new UntypedFormControl("", [Validators.required, Validators.email])
    })

    constructor(        
        private _route: ActivatedRoute,
        private _ms: MemberService, 
        private _sb: MatSnackBar,
        private _router: Router) {
    }

    async ngOnInit() {
        const params = await firstValueFrom(this._route.params)
        this._id = params["id"]

        this.member = (await this._ms.getMember(this._id)).response

        if (this.member !== undefined) {
            this.ready = true
            this.editMemberForm.get("firstName")?.setValue(this.member.firstName);
            this.editMemberForm.get("lastName")?.setValue(this.member.lastName);
            this.editMemberForm.get("username")?.setValue(this.member.userName);
            this.editMemberForm.get("email")?.setValue(this.member.email);
        }
    }

    async onSubmit() {
        this.ready = false
        const values = this.editMemberForm.value

        if( this.member )
        {
            const response = await this._ms.editMember(
                this.member.id,
                values.firstName,
                values.lastName,
                values.username,
                values.email
            )

            if (!response.error) {
                this._sb.open("Lid is aangepast", "", snackbarConfig("success"))
                this._router.navigateByUrl("/members/memberlist")
            } else {
                this.ready = true
                this._sb.open(response.error.message, "", snackbarConfig("error"))
            }
        }
        else
        {
            this.ready = true
            this._sb.open("Geen lid geselecteerd", "", snackbarConfig("error"))
        }
    }
}