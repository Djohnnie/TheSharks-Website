import { Component, OnInit } from '@angular/core';
import { Breakpoints, BreakpointObserver } from '@angular/cdk/layout';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { firstValueFrom, map, Observable, shareReplay } from 'rxjs';
import { AuthService } from 'src/app/services/auth/auth.service';
import { DocumentService } from 'src/app/services/document/document.service';
import { snackbarConfig } from 'src/config/SnackbarConfig';
import { DocumentListItemDto } from 'src/types/dto/document/DocumentListItemDto';
import { DocumentListItem } from 'src/types/application/document/DocumentListItem';
import { Claims } from 'src/types/application/role/Claims';
import { AddDocumentDialogComponent } from '../add-document-dialog/add-document-dialog.component';
import { DeleteDocumentDialogComponent } from '../delete-document-dialog/delete-document-dialog.component';

@Component({
    selector: 'app-document-list',
    templateUrl: './document-list.component.html',
    styleUrls: ['./document-list.component.scss']
})
export class DocumentListComponent implements OnInit {

    importantDocumentList: DocumentListItemDto[] | undefined
    otherDocumentList: DocumentListItemDto[] | undefined
    columns = ["name"]
    pageSize = 999

    selected: DocumentListItem | undefined
    authorized = false

    isHandset$: Observable<boolean> = this._bo.observe([Breakpoints.Small, Breakpoints.XSmall])
        .pipe(
            map(result => result.matches),
            shareReplay()
        );

    constructor(
        private _ds: DocumentService,
        public dialog: MatDialog,
        private _sb: MatSnackBar,
        private _as: AuthService,
        private _bo: BreakpointObserver
    ) { }

    async ngOnInit() {
        await this.loadDocuments()
        this.authorized = this._as.authorizeClaims([Claims.ManageDownloadables])
    }

    private async loadDocuments() {
        const documents = (await this._ds.getDocuments(1, this.pageSize)).response
        this.importantDocumentList = documents?.documents.filter(x => x.isImportant)
        this.otherDocumentList = documents?.documents.filter(x => !x.isImportant)
    }

    async openDialog() {
        const ref = this.dialog.open(AddDocumentDialogComponent)

        const result = await firstValueFrom(ref.afterClosed())

        //Check result from dialog
        if (result === undefined) return;
        if (result === "") return;
        if (result.success) {
            this._sb.open(result.text, "", snackbarConfig("success"))
            await this.loadDocuments()
        } else {
            this._sb.open(result.text, "", snackbarConfig("error"))
        }
    }

    selectDoc(doc: DocumentListItem) {
        this.selected = doc
    }

    async download() {
        const doc = (await this._ds.getDocumentUri(this.selected!.id)).response

        if (!doc) return;

        const a = document.createElement("a")
        a.download = doc.uri
        a.href = doc.uri
        document.body.appendChild(a)
        a.click()
        a.remove()

        await this.loadDocuments()
    }

    async deleteDocument() {
        const ref = this.dialog.open(DeleteDocumentDialogComponent, { data: this.selected })

        const result = await firstValueFrom(ref.afterClosed())
        if (!result) return;

        await this.loadDocuments()
    }
}
