import { Component, OnInit } from '@angular/core';
import { UntypedFormGroup, UntypedFormControl, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { ChangeEvent } from '@ckeditor/ckeditor5-angular';
import * as ClassicEditor from 'ckeditor5/build/ckeditor';
import { firstValueFrom } from 'rxjs';
import { NewsItemService } from 'src/app/services/news-item/news-item.service';
import { UserService } from 'src/app/services/user/user.service';
import { snackbarConfig } from 'src/config/SnackbarConfig';
import { NewsItem } from 'src/types/application/news/NewsItem';
import { User } from 'src/types/application/user/User';
import { size_1MB } from 'src/util/constants';

@Component({
    selector: 'app-edit-news-item',
    templateUrl: './edit-news-item.component.html',
    styleUrls: ['./edit-news-item.component.scss']
})
export class EditNewsItemComponent implements OnInit {

    editor = ClassicEditor

    titleForm = new UntypedFormGroup({
        title: new UntypedFormControl("", [Validators.required]),
        membersOnly: new UntypedFormControl(false)
    })
    content = ""

    newsItem: NewsItem | undefined
    private _user: User | undefined

    constructor(
        private _nis: NewsItemService,
        private _us: UserService,
        private _route: ActivatedRoute,
        private _sb: MatSnackBar,
        private _router: Router
    ) {

    }

    async ngOnInit() {
        const params = await firstValueFrom(this._route.params)
        const id: string = params["id"]

        this._user = (await this._us.getUser()).response
        this.newsItem = (await this._nis.getNewsItem(id)).response
        if (this.newsItem) {
            this.titleForm.get("title")?.setValue(this.newsItem.title)
            this.content = this.newsItem.content
            this.titleForm.get("membersOnly")?.setValue(this.newsItem.membersOnly)
        }
    }

    async onSubmit() {
        if (this.content.length > size_1MB) {
            this._sb.open("Inhoud is groter dan 1MB", "", snackbarConfig("error"))
            return;
        }

        const response = await this._nis.updateNewsItem(
            this.newsItem!.id,
            this.titleForm.value.title,
            this.content,
            this.newsItem!.publishDate,
            this.titleForm.value.membersOnly
        )

        if (response.error === undefined) {
            this._sb.open("Post bewerkt", "", snackbarConfig("success"))
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