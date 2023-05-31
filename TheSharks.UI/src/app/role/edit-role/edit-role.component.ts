import { Component, OnInit } from '@angular/core';
import { UntypedFormArray, UntypedFormControl, UntypedFormGroup } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute } from '@angular/router';
import { firstValueFrom } from 'rxjs';
import { RoleService } from 'src/app/services/role/role.service';
import { snackbarConfig } from 'src/config/SnackbarConfig';
import { Claim } from 'src/types/application/role/Claim';
import { Claims } from 'src/types/application/role/Claims';
import { Role } from 'src/types/application/role/Role';

@Component({
    selector: 'app-edit-role',
    templateUrl: './edit-role.component.html',
    styleUrls: ['./edit-role.component.css']
})
export class EditRoleComponent implements OnInit {

    role: Role | undefined
    private _id = ""
    claimValues = Object.values(Claims)
    private claimKeys = Object.keys(Claims)

    roleForm = new UntypedFormGroup({
        claims: new UntypedFormArray([])
    })

    get roleClaims() {
        return this.roleForm.get("claims") as UntypedFormArray
    }

    constructor(
        private _rs: RoleService,
        private _route: ActivatedRoute,
        private _sb: MatSnackBar
    ) { }

    async ngOnInit() {
        //Get role
        const params = await firstValueFrom(this._route.params)
        this._id = params["id"]
        this.role = (await this._rs.getRole(this._id)).response

        //Load claims
        this.claimKeys.forEach((claim) => {
            const temp = this.role?.claims.find((c) => c.claimName === claim)

            if (temp) {
                this.roleClaims.push(new UntypedFormControl(true))
            } else {
                this.roleClaims.push(new UntypedFormControl(false))
            }
        })
    }

    async onSave() {
        const list: Claim[] = this.roleClaims.controls.map((v, i) => {
            return {
                claimName: this.claimKeys[i],
                isChecked: v.value
            }
        })

        const response = await this._rs.updateRole(this._id, list)

        if (response.error === undefined) {
            this._sb.open("Rol aangepast", "", snackbarConfig("success"))
            this.role = (await this._rs.getRole(this._id)).response
        } else {
            this._sb.open(response.error.message, "", snackbarConfig("error"))
        }

        this.roleForm.markAsPristine()
    }
}
