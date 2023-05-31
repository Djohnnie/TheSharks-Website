import { Component, OnInit } from '@angular/core';
import { FormArray, UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { firstValueFrom } from 'rxjs';
import { CmsService } from 'src/app/services/cms/cms.service';
import { Page } from 'src/types/application/cms/Page';
import * as ClassicEditor from 'ckeditor5/build/ckeditor';
import { ChangeEvent } from '@ckeditor/ckeditor5-angular';
import { CmsComponent } from 'src/types/application/cms/CmsComponent';
import { MatSnackBar } from '@angular/material/snack-bar';
import { snackbarConfig } from 'src/config/SnackbarConfig';
import { size_3MB } from 'src/util/constants';
import { Components } from 'src/types/application/cms/Components';
import { MatDialog } from '@angular/material/dialog';
import { ComponentDialogComponent } from '../component-dialog/component-dialog.component';

@Component({
    selector: 'app-edit-page',
    templateUrl: './edit-page.component.html',
    styleUrls: ['./edit-page.component.scss'],
})
export class EditPageComponent implements OnInit {

    page: Page | undefined

    editPageForm = new UntypedFormGroup({
        title: new UntypedFormControl("", [Validators.required]),
        link: new UntypedFormControl("", [Validators.required]),
        isOnlyAvailableForMembers: new UntypedFormControl(false),
        isDefaultPage: new UntypedFormControl(false),
        isDefaultPageForMembers: new UntypedFormControl(false),
    })
    editor = ClassicEditor

    componentLeft: CmsComponent | undefined
    componentRight: CmsComponent | undefined

    loadingUpdate = false

    get componentTypes(): typeof Components {
        return Components
    }

    left = -1;
    right = -2;

    constructor(
        private _cmss: CmsService,
        private _route: ActivatedRoute,
        private _sb: MatSnackBar,
        private _router: Router,
        public dialog: MatDialog
    ) { }

    async ngOnInit() {
        await this.loadPageData()
    }

    private async loadPageData() {
        const params = await firstValueFrom(this._route.params)

        const response = await this._cmss.getPage(params["link"])
        this.page = response.response
        if (!this.page) return;

        this.editPageForm.get("title")?.setValue(this.page?.title)
        this.editPageForm.get("link")?.setValue(this.page?.link)
        this.editPageForm.get("isOnlyAvailableForMembers")?.setValue(this.page?.isOnlyAvailableForMembers)
        this.editPageForm.get("isDefaultPage")?.setValue(this.page?.isDefaultPage)
        this.editPageForm.get("isDefaultPageForMembers")?.setValue(this.page?.isDefaultPageForMembers)
        this.page.components = this.page.components.sort((a, b) => a.position - b.position)

        //If page has no side components, stop
        switch (this.page.components.filter(c => c.position < 0).length) {
            case 0: return;
            case 1: {
                const c = this.page.components.shift()
                if (c?.position === -1) this.componentLeft = c
                else this.componentRight = c
            } break;
            case 2: {
                this.componentRight = this.page.components.shift()
                this.componentLeft = this.page.components.shift()
            } break;
        }
    }

    onEditorChange(position: number, event: ChangeEvent) {
        try {
            this.getComponentByPosition(position)!.content = event.editor.getData()
        } catch (e) { }
    }

    onComponentChange(position: number, value: any) {
        if (value instanceof Event) return;
        this.getComponentByPosition(position)!.content = JSON.stringify(value)
    }

    async chooseComponent(position: number) {
        const ref = this.dialog.open(ComponentDialogComponent)
        const result = await firstValueFrom(ref.afterClosed())
        if (!result) return;
        if (!this.page) return;

        const c: CmsComponent = {
            name: result,
            content: "",
            position: position
        }

        if (position === this.left) {
            //If added left
            this.componentLeft = c
        } else if (position === this.right) {
            //If added right
            this.componentRight = c
        } else {
            //If added middle
            //Move all components in the middle depending on the newly added component's position
            for (let i = 0; i < this.page.components.filter(c => c.position >= position).length; i++) {
                this.page.components[i].position += 1;
            }
            this.page.components.push(c)
        }

        this.updatePositions()
    }

    deleteComponent(position: number) {
        if (position === this.left) this.componentLeft = undefined
        else if (position === this.right) this.componentRight = undefined
        else this.page!.components = this.page!.components.filter((c) => c.position !== position)
        this.updatePositions()
    }

    moveComponent(index: number, delta: number) {
        this.page!.components[index + delta].position = this.page!.components[index].position
        this.page!.components[index].position += delta
        this.updatePositions()
    }

    private updatePositions() {
        this.page!.components = this.page!.components.sort((a, b) => a.position - b.position)
        for (let i = 0; i < this.page!.components.length; i++) {
            this.page!.components[i].position = i
        }
    }

    private getComponentByPosition(position: number) {
        if (position === this.left) return this.componentLeft
        else if (position === this.right) return this.componentRight
        else return this.page!.components.find(c => c.position === position)
    }

    async onSave() {
        if (!this.page) return;
        const totalContent = this.page.components.map(c => c.content).join()
        if (totalContent.length > size_3MB) {
            this._sb.open("Inhoud is groter dan 3MB", "", snackbarConfig("error"))
            return;
        }

        if (this.componentLeft) this.page.components.push(this.componentLeft)
        if (this.componentRight) this.page.components.push(this.componentRight)

        this.loadingUpdate = true
        const response = await this._cmss.updatePage(
            this.page!.id,
            this.editPageForm.value.title,
            this.editPageForm.value.link,
            this.editPageForm.value.isOnlyAvailableForMembers,
            this.editPageForm.value.isDefaultPage,
            this.editPageForm.value.isDefaultPageForMembers,
            this.page.components,
            this.page.navBarPosition,
            this.page.navBarSubPosition
        )

        if (response.error === undefined) {
            this._sb.open("Pagina gewijzigd", "", snackbarConfig("success"))
            this._cmss.contentChanged()
            this._router.navigateByUrl("/page/" + this.editPageForm.value.link)
        } else {
            this._sb.open(response.error.message, "", snackbarConfig("error"))
        }
        this.loadingUpdate = false
    }
}
