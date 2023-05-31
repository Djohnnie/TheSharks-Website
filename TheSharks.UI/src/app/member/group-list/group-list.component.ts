import { SelectionModel } from '@angular/cdk/collections';
import { Breakpoints, BreakpointObserver } from '@angular/cdk/layout';
import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatTableDataSource } from '@angular/material/table';
import { map, Observable, shareReplay } from 'rxjs';
import { AuthService } from 'src/app/services/auth/auth.service';
import { MemberService } from 'src/app/services/member/member.service';
import { RoleService } from 'src/app/services/role/role.service';
import { MemberListItem } from 'src/types/application/member/MemberListItem';
import { Claims } from 'src/types/application/role/Claims';
import { RoleListItem } from 'src/types/application/role/RoleListItem';
import { EmailDialogComponent } from '../email-dialog/email-dialog.component';

@Component({
    selector: 'app-group-list',
    templateUrl: './group-list.component.html',
    styleUrls: ['./group-list.component.scss']
})
export class GroupListComponent implements OnInit {

    dataSource: MatTableDataSource<RoleListItem> | undefined
    selection = new SelectionModel<RoleListItem>(true, []);
    columns = ["select", "name"]

    authorized = false

    isHandset$: Observable<boolean> = this._bo.observe([Breakpoints.Small, Breakpoints.XSmall])
        .pipe(
            map(result => result.matches),
            shareReplay()
        );

    constructor(
        private _rs: RoleService,
        private _as: AuthService,
        public dialog: MatDialog,
        private _bo: BreakpointObserver
    ) { }

    async ngOnInit() {
        const response = (await this._rs.getRoles()).response

        this.dataSource = new MatTableDataSource(response?.filter( x => x.name !== "Technisch" ));

        await this.checkClaims()
    }

    private async checkClaims() {
        this.authorized = await this._as.authorizeClaims([Claims.ManageMembers])
    }

    /** Whether the number of selected elements matches the total number of rows. */
    isAllSelected() {
        const numSelected = this.selection.selected.length;
        const numRows = this.dataSource!.data.length;
        return numSelected === numRows;
    }

    getSelectedCount() {
        return this.selection.selected.map(m => m.memberCount).reduce((a, b) => a + b, 0)
    }

    /** Selects all rows if they are not all selected; otherwise clear selection. */
    masterToggle() {
        if (this.isAllSelected()) {
            this.selection.clear();
            return;
        }

        this.selection.select(...this.dataSource!.data);
    }

    openEmailDialog() {
        const id = this._as.getDecodedToken().sub

        this.dialog.open(EmailDialogComponent, {
            width: "80%",
            disableClose: true,
            data: {
                id: id,
                selected: this.selection.selected.map(m => m.id),
                areMembers: false
            }
        })
    }
}
