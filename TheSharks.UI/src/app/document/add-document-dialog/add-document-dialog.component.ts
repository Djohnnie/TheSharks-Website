import { Component, OnInit } from '@angular/core';
import { UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { DocumentService } from 'src/app/services/document/document.service';
import { snackbarConfig } from 'src/config/SnackbarConfig';
import { size_10MB, size_2MB } from 'src/util/constants';

@Component({
    selector: 'app-add-document-dialog',
    templateUrl: './add-document-dialog.component.html',
    styleUrls: ['./add-document-dialog.component.scss']
})
export class AddDocumentDialogComponent implements OnInit {

    addDocumentForm = new UntypedFormGroup({
        name: new UntypedFormControl("", [Validators.required]),
        isImportant: new UntypedFormControl(false, [Validators.required]),
        file: new UntypedFormControl(null, [Validators.required])
    })

    fileName = ""

    loadingCall = false

    constructor(
        private _ds: DocumentService,
        private _sb: MatSnackBar,
        public ref: MatDialogRef<AddDocumentDialogComponent>
    ) { }

    ngOnInit(): void {
    }

    fileBrowseHandler(fileUploader: HTMLInputElement) {
        //Preview image and save change to form
        if (fileUploader.files?.length === undefined) return;
        if (fileUploader.files?.length <= 0) return;

        const file = fileUploader.files[0];

        if (file.size > size_10MB) {
            this._sb.open("Het gekozen bestand is te groot", "", snackbarConfig("error"))
        } else {
            this.addDocumentForm.get("file")!.setValue(file)
            this.fileName = file.name
        }
    }

    async save() {
        if (!this.addDocumentForm.valid) return;

        this.loadingCall = true

        const response = await this._ds.addDocument(
            this.addDocumentForm.value.name,
            this.addDocumentForm.value.isImportant,
            this.addDocumentForm.value.file
        )

        if (response.error === undefined) {
            this.ref.close({ success: true, text: "Pagina aangemaakt" })
        } else {
            this.ref.close({ success: false, text: response.error.message })
        }

        this.loadingCall = false
    }
}
