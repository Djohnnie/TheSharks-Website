import { Component, Inject, OnInit } from '@angular/core';
import { UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MemberService } from 'src/app/services/member/member.service';
import { User } from 'src/types/application/user/User';
import { UserService } from 'src/app/services/user/user.service';
import * as ClassicEditor from 'ckeditor5/build/ckeditor';
import { ChangeEvent } from '@ckeditor/ckeditor5-angular';

interface DialogData {
    id: string
    selected: string[]
    areMembers: boolean
    subject: string
}

@Component({
    selector: 'app-email-dialog',
    templateUrl: './email-dialog.component.html',
    styleUrls: ['./email-dialog.component.scss']
})
export class EmailDialogComponent implements OnInit {

    private _user: User | undefined

    emailForm = new UntypedFormGroup({
        subject: new UntypedFormControl("", [Validators.required]),
        message: new UntypedFormControl(""),
    })

    editor = ClassicEditor

    loading = false
    emptyMessage = true

    constructor(
        @Inject(MAT_DIALOG_DATA) private data: DialogData,
        private _ms: MemberService,
        private _us: UserService,
        private ref: MatDialogRef<EmailDialogComponent>,
    ) { }

    async ngOnInit() {
        this._user = (await this._us.getUser()).response
        this.emailForm.get("subject")?.setValue(this.data.subject)
    }

    async sendEmail() {
        if (!this.emailForm.valid || !this._user) return;

        this.loading = true

        const response = await this._ms.sendEmail(
            this.data.id,
            this._user.id,
            this.emailForm.value.subject,
            this.emailForm.value.message,
            this.data.areMembers ? this.data.selected : [],
            !this.data.areMembers ? this.data.selected : [],
        )

        if (response.error === undefined) {
            this.ref.close({ success: true, text: "Email verstuurd" })
        } else {
            this.ref.close({ success: false, text: response.error.message })
        }

        this.loading = false
    }

    onEditorChange(event: ChangeEvent) {
        try {
            this.emailForm.value.message = event.editor.getData()
            this.emptyMessage = !this.emailForm.value.message
        } catch (e) { }
    }
}