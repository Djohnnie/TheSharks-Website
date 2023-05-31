import { SelectionModel } from '@angular/cdk/collections';
import { Breakpoints, BreakpointObserver } from '@angular/cdk/layout';
import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { firstValueFrom, map, Observable, shareReplay } from 'rxjs';
import { AuthService } from 'src/app/services/auth/auth.service';
import { UserService } from '../../services/user/user.service';
import { MemberService } from 'src/app/services/member/member.service';
import { MemberListItemWithRoles } from 'src/types/application/member/MemberListItemWithRoles';
import { Claims } from 'src/types/application/role/Claims';
import { EmailDialogComponent } from '../email-dialog/email-dialog.component';
import { DeleteMemberDialogComponent } from '../delete-member-dialog/delete-member-dialog.component';
import { User } from 'src/types/application/user/User';

interface MemberItemPicture extends MemberListItemWithRoles {
    picture: string
}

@Component({
    selector: 'app-member-list',
    templateUrl: './member-list.component.html',
    styleUrls: ['./member-list.component.scss']
})

export class MemberListComponent implements OnInit {

    dataSource: MatTableDataSource<MemberItemPicture> | undefined
    selection = new SelectionModel<MemberListItemWithRoles>(true, []);
    columns = ["select", "name"]
    currentUser: User | undefined

    authorized = false
    memberManager = false

    isHandset$: Observable<boolean> = this._bo.observe([Breakpoints.Small, Breakpoints.XSmall])
        .pipe(
            map(result => result.matches),
            shareReplay()
        );

    constructor(
        private _ms: MemberService,
        private _as: AuthService,
        private _us: UserService,
        public dialog: MatDialog,
        private _bo: BreakpointObserver,
        private _router: Router
    ) { }

    async ngOnInit() {

        this.currentUser = (await this._us.getUser()).response;
        await this.checkClaims()
        
        await this.loadMembers();
    }

    private async loadMembers() {
        const response = (await this._ms.getAllMembersWithRoles()).response

        if (response) {
            
            this.dataSource = new MatTableDataSource(response.filter( f => this.memberManager || this.currentUser?.id == f.id || !f.roles.some( s => s.name === "Technisch") )
                .map(m => {
                    return { ...m, picture: "/assets/person-placeholder.jpg" }
            }))

            this.dataSource.filterPredicate = function customFilter(data , filter: string) : boolean {
                return data.firstName.toLowerCase().includes(filter) || data.lastName.toLowerCase().includes(filter);
            }

            this.getProfilePictures()
        }
    }

    private async getProfilePictures() {
        for (let i = 0; i < this.dataSource!.data.length; i++) {
            const m = this.dataSource!.data[i]
            const pic = (await this._ms.getProfilePicture(m.id)).response
            this.dataSource!.data[i].picture = pic ? "data:image/png;base64," + pic : "/assets/person-placeholder.jpg"
        }
    }

    private async checkClaims() {
        this.authorized = await this._as.authorizeClaims([Claims.ManageMembers])
        this.memberManager = await this._as.authorizeClaims([Claims.ManageMembers])
    }

    roleClicked(event: any, role: any) {
        let toSelect = new Array<MemberListItemWithRoles>();

        this.dataSource!.filteredData.forEach(member=>{
            if(member.roles.some(x => x.id === role.id)) {
                toSelect.push(member);
            }
        });

        this.selection.select(...toSelect);

        event.stopPropagation();
    }

    applyFilter(event: Event) {
        const rowsBefore = this.dataSource!.filteredData.length;
        const filterValue = (event.target as HTMLInputElement).value
        this.dataSource!.filter = filterValue.trim().toLowerCase();        
        const rowsAfter = this.dataSource!.filteredData.length;

        if(rowsAfter < rowsBefore) {
            this.selection.clear();
        }
    }

    /** Whether the number of selected elements matches the total number of rows. */
    isAllSelected() {
        const numSelected = this.selection.selected.length;
        const numRows = this.dataSource!.filteredData.length;
        return numSelected === numRows;
    }

    /** Selects all rows if they are not all selected; otherwise clear selection. */
    masterToggle() {
        if (this.isAllSelected()) {
            this.selection.clear();
            return;
        }

        this.selection.select(...this.dataSource!.filteredData);
    }

    editSelectedMember() {
        this._router.navigateByUrl("/editmember/" + this.selection.selected[0].id)
    }

    async removeSelectedMember() {
        const ref = this.dialog.open(DeleteMemberDialogComponent, { data: this.selection.selected[0].id })

        if (await firstValueFrom(ref.afterClosed())) {
            await this.loadMembers();
        }
    }

    openEmailDialog() {
        const id = this._as.getDecodedToken().sub

        this.dialog.open(EmailDialogComponent, {
            width: "80%",
            disableClose: true,
            data: {
                id: id,
                selected: this.selection.selected.map(m => m.id),
                areMembers: true
            }
        })
    }
}