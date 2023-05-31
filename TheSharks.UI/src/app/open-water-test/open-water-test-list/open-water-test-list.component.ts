import { SelectionModel } from '@angular/cdk/collections';
import { Breakpoints, BreakpointObserver } from '@angular/cdk/layout';
import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { map, Observable, shareReplay, firstValueFrom } from 'rxjs';
import { AuthService } from 'src/app/services/auth/auth.service';
import { UserService } from '../../services/user/user.service';
import { OpenWaterTestService } from 'src/app/services/open-water-test/open-water-test.service';
import { OpenWaterTestDto } from 'src/types/dto/open-water-test/OpenWaterTestDto';
import { Claims } from 'src/types/application/role/Claims';
import { OpenWaterTestContentDialogComponent } from '../open-water-test-content-dialog/open-water-test-content-dialog.component'
import { DeleteOpenWaterTestDialogComponent } from "../delete-open-water-test-dialog/delete-open-water-test-dialog.component";

@Component({
    selector: 'open-water-test-list',
    templateUrl: './open-water-test-list.component.html',
    styleUrls: ['./open-water-test-list.component.scss']
})

export class OpenWaterTestListComponent implements OnInit {

    dataSource: MatTableDataSource<OpenWaterTestDto> | undefined
    selection = new SelectionModel<OpenWaterTestDto>(true, []);
    columns = ["select", "title"]

    authorized = false
    memberManager = false
    loading = true;

    isHandset$: Observable<boolean> = this._bo.observe([Breakpoints.Small, Breakpoints.XSmall])
        .pipe(
            map(result => result.matches),
            shareReplay()
        );

    constructor(
        private _ows: OpenWaterTestService,
        private _as: AuthService,
        private _us: UserService,
        public dialog: MatDialog,
        private _bo: BreakpointObserver,
        private _router: Router
    ) { }

    async ngOnInit() {
        await this.checkClaims()        
        await this.loadOpenWaterTests()
    }

    private async loadOpenWaterTests() {
        this.loading = true

        const response = (await this._ows.getAllOpenWaterTests("")).response

        if (response) {            
            this.dataSource = new MatTableDataSource(response.openWaterTests);
        }

        this.loading = false
    }

    private async checkClaims() {
        this.authorized = await this._as.authorizeClaims([Claims.ManageMembers])
        this.memberManager = await this._as.authorizeClaims([Claims.ManageMembers])
    }

    isAllSelected() {
        const numSelected = this.selection.selected.length;
        const numRows = this.dataSource!.data.length;
        return numSelected === numRows;
    }

    masterToggle() {
        if (this.isAllSelected()) {
            this.selection.clear();
            return;
        }

        this.selection.select(...this.dataSource!.filteredData);
    }

    openContentDialog(openWaterTest: OpenWaterTestDto) {
        this.dialog.open(OpenWaterTestContentDialogComponent, {
            data: openWaterTest
        })
    }

    editSelectedTest() {
        this._router.navigateByUrl("/open-water-tests/edit/" + this.selection.selected[0].id)
    }

    async removeSelectedTests() {

        const ref = this.dialog.open(DeleteOpenWaterTestDialogComponent)

        if (await firstValueFrom(ref.afterClosed())) {

            this.loading = true

            for (const selected of this.selection.selected) {
                await this._ows.removeOpenWaterTest(selected.id)
            }
            
            await this.loadOpenWaterTests()
            this.selection.clear();

            this.loading = false
        }
    }
}