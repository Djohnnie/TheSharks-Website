import { Component, OnInit } from '@angular/core';
import { Breakpoints, BreakpointObserver } from '@angular/cdk/layout';
import { firstValueFrom, map, Observable, shareReplay, tap } from 'rxjs';
import { MatDialog } from '@angular/material/dialog';
import { RoleService } from 'src/app/services/role/role.service';
import { RoleListItem } from 'src/types/application/role/RoleListItem';
import { DeleteRoleDialogComponent } from '../delete-role-dialog/delete-role-dialog.component';

@Component({
    selector: 'app-manage-roles',
    templateUrl: './manage-roles.component.html',
    styleUrls: ['./manage-roles.component.scss']
})
export class ManageRolesComponent implements OnInit {

    roles: RoleListItem[] | undefined
    columns = ["name"]    

    isHandset$: Observable<boolean> = this._bo.observe([Breakpoints.Small, Breakpoints.XSmall])
        .pipe(
            map(result => result.matches),
            shareReplay()
        );

    constructor(
        private _rs: RoleService, 
        public dialog: MatDialog,
        private _bo: BreakpointObserver,) { }

    async ngOnInit() {
        this.roles = (await this._rs.getRoles()).response
    }

    openDialog(role: RoleListItem) {
        this.dialog.open(DeleteRoleDialogComponent, {
            data: {
                name: role.name,
                id: role.id
            }
        })
    }
}
