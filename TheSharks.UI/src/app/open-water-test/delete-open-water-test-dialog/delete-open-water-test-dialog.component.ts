import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
    selector: 'delete-open-water-test-dialog',
    templateUrl: './delete-open-water-test-dialog.component.html',
    styleUrls: ['./delete-open-water-test-dialog.component.scss']
})
export class DeleteOpenWaterTestDialogComponent implements OnInit {

    constructor(
        @Inject(MAT_DIALOG_DATA) public data: string,
        public ref: MatDialogRef<DeleteOpenWaterTestDialogComponent>
    ) { }

    ngOnInit(): void {
    }

    async onDelete() {
        this.ref.close(true)
    }
}