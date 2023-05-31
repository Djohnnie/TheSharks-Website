import { Component, OnInit } from '@angular/core';
import { AbstractControl, UntypedFormArray, UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
import { MatChip } from '@angular/material/chips';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute } from '@angular/router';
import { firstValueFrom } from 'rxjs';
import { MemberService } from 'src/app/services/member/member.service';
import { snackbarConfig } from 'src/config/SnackbarConfig';
import { MemberRole } from 'src/types/application/role/MemberRole';
import { MemberRoles } from 'src/types/application/role/MemberRoles';
import { RoleUpdate } from 'src/types/application/role/RoleUpdate';

@Component({
    selector: 'app-member-details',
    templateUrl: './member-details.component.html',
    styleUrls: ['./member-details.component.scss']
})
export class MemberDetailsComponent implements OnInit {

    memberRoles: MemberRoles | undefined
    private _id = ""

    roleForm = new UntypedFormGroup({
        divingRole: new UntypedFormControl("", [Validators.required]),
        nonDivingRoles: new UntypedFormArray([])
    })

    get nonDivingRoles() {
        return this.roleForm.get("nonDivingRoles") as UntypedFormArray
    }

    constructor(
        private _ms: MemberService,
        private _route: ActivatedRoute,
        private _sb: MatSnackBar
    ) { }

    async ngOnInit() {
        const params = await firstValueFrom(this._route.params)
        this._id = params["id"]

        this.memberRoles = (await this._ms.getMemberRoles(this._id)).response

        if (this.memberRoles) {
            this.loadRoles()
            this.setCurrentDiveCertificate()
        }
    }

    private loadRoles() {
        this.memberRoles!.nonDiveCertificateRoles.forEach((r) => {
            this.nonDivingRoles.push(new UntypedFormControl(r.isAssignedToMember))
        })
    }

    setCurrentDiveCertificate() {
        this.roleForm.get("divingRole")?.setValue(this.memberRoles?.diveCertificateRoles.find(r => r.isAssignedToMember)?.name)
    }

    async onSubmit() {
        const cert: RoleUpdate = {
            roleName: this.roleForm.value.divingRole,
            isChecked: true
        }

        const roles: RoleUpdate[] = this.nonDivingRoles.controls.map((v, i) => {
            return {
                roleName: this.memberRoles!.nonDiveCertificateRoles[i].name,
                isChecked: v.value
            }
        })

        const response = await this._ms.updateMemberRoles(this._id, cert, roles)

        if (response.error === undefined) {
            this._sb.open("Lid aangepast", "", snackbarConfig("success"))
            this.memberRoles = (await this._ms.getMemberRoles(this._id)).response
        } else {
            this._sb.open(response.error.message, "", snackbarConfig("error"))
        }

        this.roleForm.markAsPristine()
    }

    toggleSelection(chip: MatChip, control: AbstractControl) {
        chip.toggleSelected()
        control.setValue(chip.selected)
        this.roleForm.markAsDirty()
    }
}
