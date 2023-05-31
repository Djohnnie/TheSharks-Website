import { Component, OnInit } from '@angular/core';
import { UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { ChangeEvent } from '@ckeditor/ckeditor5-angular/ckeditor.component';
import * as ClassicEditor from 'ckeditor5/build/ckeditor';
import { NewsItemService } from 'src/app/services/news-item/news-item.service';
import { UserService } from 'src/app/services/user/user.service';
import { snackbarConfig } from 'src/config/SnackbarConfig';
import { User } from 'src/types/application/user/User';
import { size_1MB } from 'src/util/constants';

@Component({
    selector: 'app-add-news-item',
    templateUrl: './add-news-item.component.html',
    styleUrls: ['./add-news-item.component.scss']
})
export class AddNewsItemComponent implements OnInit {

    editor = ClassicEditor

    titleForm = new UntypedFormGroup({
        title: new UntypedFormControl("", [Validators.required]),
        membersOnly: new UntypedFormControl(false)
    })
    content = ""

    private _user: User | undefined

    constructor(
        private _nis: NewsItemService,
        private _us: UserService,
        private _router: Router,
        private _sb: MatSnackBar
    ) {

    }

    async ngOnInit() {
        this._user = (await this._us.getUser()).response
    }

    async onSubmit() {
        if (this.content.length > size_1MB) {
            this._sb.open("Inhoud is groter dan 1MB", "", snackbarConfig("error"))
            return;
        }

        const response = await this._nis.addNewsItem(this._user!.id, this.titleForm.value.title, this.content, this.titleForm.value.membersOnly)

        if (response.error === undefined) {
            this._sb.open("Post aangemaakt", "", snackbarConfig("success"))
            this._router.navigateByUrl("/news-items")
        } else {
            this._sb.open(response.error.message, "", snackbarConfig("error"))
        }
    }

    onChange(event: ChangeEvent) {
        try {
            this.content = event.editor.getData()
        } catch (e) { }
    }
}
