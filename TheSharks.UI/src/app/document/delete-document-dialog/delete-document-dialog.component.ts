import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { DocumentService } from 'src/app/services/document/document.service';
import { snackbarConfig } from 'src/config/SnackbarConfig';
import { DocumentListItem } from 'src/types/application/document/DocumentListItem';

@Component({
    selector: 'app-delete-document-dialog',
    templateUrl: './delete-document-dialog.component.html',
    styleUrls: ['./delete-document-dialog.component.scss']
})
export class DeleteDocumentDialogComponent implements OnInit {

    constructor(
        private _sb: MatSnackBar,
        private _ds: DocumentService,
        public ref: MatDialogRef<DeleteDocumentDialogComponent>,
        @Inject(MAT_DIALOG_DATA) public data: DocumentListItem
    ) { }

    ngOnInit(): void {
    }

    async onDelete() {
        const response = await this._ds.deleteDocument(this.data.id, this.data.fileName)

        if (response.error === undefined) {
            this._sb.open("Activiteit verwijderd", "", snackbarConfig("success"))
            this.ref.close(true)
        } else {
            this._sb.open(response.error.message, "", snackbarConfig("error"))
        }
    }
}
