import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { NewsItemService } from 'src/app/services/news-item/news-item.service';
import { snackbarConfig } from 'src/config/SnackbarConfig';

@Component({
    selector: 'app-delete-news-item-dialog',
    templateUrl: './delete-news-item-dialog.component.html',
    styleUrls: ['./delete-news-item-dialog.component.scss']
})
export class DeleteNewsItemDialogComponent implements OnInit {

    constructor(
        @Inject(MAT_DIALOG_DATA) public data: string,
        private _nis: NewsItemService,
        private _sb: MatSnackBar,
        private _router: Router,
        public ref: MatDialogRef<DeleteNewsItemDialogComponent>
    ) { }

    ngOnInit(): void {
    }

    async onDelete() {
        const response = await this._nis.deleteNewsItem(this.data)

        if (response.error === undefined) {
            this._sb.open("Activiteit verwijderd", "", snackbarConfig("success"))
            this.ref.close(true)
        } else {
            this._sb.open(response.error.message, "", snackbarConfig("error"))
        }
    }
}
