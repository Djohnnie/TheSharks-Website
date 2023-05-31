import { Component, OnInit } from '@angular/core';
import { UntypedFormArray, UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { RoleService } from 'src/app/services/role/role.service';
import { snackbarConfig } from 'src/config/SnackbarConfig';
import { Claim } from 'src/types/application/role/Claim';
import { Claims } from 'src/types/application/role/Claims';

@Component({
    selector: 'app-add-role',
    templateUrl: './add-role.component.html',
    styleUrls: ['./add-role.component.css']
})
export class AddRoleComponent implements OnInit {

    claimValues = Object.values(Claims)
    private claimKeys = Object.keys(Claims)

    roleForm = new UntypedFormGroup({
        name: new UntypedFormControl("", [Validators.required]),
        certificate: new UntypedFormControl(false, [Validators.required]),
        claims: new UntypedFormArray([])
    })

    get roleClaims() {
        return this.roleForm.get("claims") as UntypedFormArray
    }

    constructor(
        private _rs: RoleService,
        private _sb: MatSnackBar,
        private _router: Router
    ) {
        this.claimValues.forEach(_ => this.roleClaims.push(new UntypedFormControl(false)))
    }

    ngOnInit(): void {
    }

    async onSubmit() {
        const list: Claim[] = this.roleClaims.controls.map((v, i) => {
            return {
                claimName: this.claimKeys[i],
                isChecked: v.value
            }
        })

        const response = await this._rs.addRole(
            this.roleForm.value.name,
            this.roleForm.value.certificate,
            list
        )

        if (response.error === undefined) {
            this._sb.open("Rol aangemaakt", "", snackbarConfig("success"))
            this._router.navigateByUrl("/roles")
        } else {
            this._sb.open(response.error.message, "", snackbarConfig("error"))
        }
    }
}
