import { Component, OnInit } from '@angular/core';
import { AbstractControl, UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { CmsService } from 'src/app/services/cms/cms.service';

@Component({
    selector: 'app-add-page-dialog',
    templateUrl: './add-page-dialog.component.html',
    styleUrls: ['./add-page-dialog.component.scss']
})
export class AddPageDialogComponent implements OnInit {

    loading = false;

    pageForm = new UntypedFormGroup({
        title: new UntypedFormControl("", [Validators.required]),
        link: new UntypedFormControl("", [
            Validators.required,
            Validators.pattern(/^[A-Za-z0-9]*$/),
            Validators.minLength(2)
        ])
    })

    constructor(
        public ref: MatDialogRef<AddPageDialogComponent>,
        private _cmss: CmsService
    ) { }

    linkRequired(control: AbstractControl) {
        if (!control.parent) return null;
        if (control.parent.get("hasLink")?.value) return [
        ]
        return null
    }

    linkError() {
        const link = this.pageForm.get("link")!
        if (link.hasError("minlength")) return "Minstens 2 tekens"
        if (link.hasError("pattern")) return "Geen speciale tekens toegelaten"
        return ""
    }

    ngOnInit(): void {
    }

    async onSubmit() {
        if (!this.pageForm.valid) return;

        this.loading = true

        const response = await this._cmss.addPage(
            this.pageForm.value.title,
            this.pageForm.value.link
        )

        if (response.error === undefined) {
            this.ref.close({ success: true, text: "Pagina aangemaakt" })
        } else {
            this.ref.close({ success: false, text: response.error.message })
        }

        this.loading = false
    }
}
