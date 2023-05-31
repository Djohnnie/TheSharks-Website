import { Component, OnInit } from '@angular/core';
import { Breakpoints, BreakpointObserver } from '@angular/cdk/layout';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { firstValueFrom, map, Observable, shareReplay } from 'rxjs';
import { CmsService } from 'src/app/services/cms/cms.service';
import { snackbarConfig } from 'src/config/SnackbarConfig';
import { PageListItem } from 'src/types/application/cms/PageListItem';
import { AddPageDialogComponent } from '../add-page-dialog/add-page-dialog.component';

@Component({
    selector: 'app-manage-pages',
    templateUrl: './manage-pages.component.html',
    styleUrls: ['./manage-pages.component.scss']
})
export class ManagePagesComponent implements OnInit {

    pageList: PageListItem[] | undefined

    isHandset$: Observable<boolean> = this._bo.observe([Breakpoints.Small, Breakpoints.XSmall])
        .pipe(
            map(result => result.matches),
            shareReplay()
        );

    constructor(
        private _cmss: CmsService,
        public dialog: MatDialog,
        private _sb: MatSnackBar,
        private _bo: BreakpointObserver
    ) { }

    async ngOnInit() {
        const response = await this._cmss.getPages()

        this.pageList = response.response
    }

    async openAddPageDialog() {
        const ref = this.dialog.open(AddPageDialogComponent, {
            width: "300px"
        })

        const result = await firstValueFrom(ref.afterClosed())

        //Check result from dialog
        if (result === undefined) return;
        if (result === "") return;
        if (result.success) {
            this._sb.open(result.text, "", snackbarConfig("success"))
            const response = await this._cmss.getPages()
            this.pageList = response.response
            this._cmss.contentChanged()
        } else {
            this._sb.open(result.text, "", snackbarConfig("error"))
        }
    }

    async onDelete(id: string) {
        await this._cmss.removePage(id)
        const response = await this._cmss.getPages()
        this.pageList = response.response
        this._cmss.contentChanged()
    }
}
