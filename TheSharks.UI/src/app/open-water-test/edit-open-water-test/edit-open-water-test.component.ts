import { Component, OnInit } from '@angular/core';
import { UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { firstValueFrom } from 'rxjs';
import { RoleService } from 'src/app/services/role/role.service';
import { OpenWaterTestService } from 'src/app/services/open-water-test/open-water-test.service';
import { DiveCertificateDto } from 'src/types/dto/role/DiveCertificateDto';
import { OpenWaterTestDto } from 'src/types/dto/open-water-test/OpenWaterTestDto';
import { snackbarConfig } from 'src/config/SnackbarConfig';
import * as ClassicEditor from 'ckeditor5/build/ckeditor';
import { ChangeEvent } from '@ckeditor/ckeditor5-angular';

@Component({
    selector: 'edit-open-water-test',
    templateUrl: './edit-open-water-test.component.html',
    styleUrls: ['./edit-open-water-test.component.scss']
})
export class EditOpenWaterTestComponent implements OnInit {
    
    ready = false
    openWaterTest: OpenWaterTestDto | undefined

    diveCertificatesList: DiveCertificateDto[] | undefined

    addOpenWaterTestForm = new UntypedFormGroup({
        title: new UntypedFormControl("", [Validators.required]),
        diveCertificate: new UntypedFormControl("", [Validators.required]),
        content: new UntypedFormControl("")
    })

    editor = ClassicEditor
    emptyContent = true

    constructor(        
        private _route: ActivatedRoute,
        private _ows: OpenWaterTestService,
        private _rs: RoleService,
        private _sb: MatSnackBar,
        private _router: Router) {
    }

    async ngOnInit() {
        this.diveCertificatesList = (await this._rs.getRoles()).response
        
        const params = await firstValueFrom(this._route.params)

        const response = await this._ows.getOpenWaterTest(params["id"])
        this.openWaterTest = response.response

        if (this.openWaterTest) {
            this.addOpenWaterTestForm.get("title")?.setValue(this.openWaterTest?.title)
            this.addOpenWaterTestForm.get("diveCertificate")?.setValue(this.openWaterTest?.diveCertificate)
            this.addOpenWaterTestForm.get("content")?.setValue(this.openWaterTest?.content)
            this.emptyContent = !this.addOpenWaterTestForm.value.content
        }

        this.ready = true
    }

    async onSubmit() {
        this.ready = false
        const values = this.addOpenWaterTestForm.value

        const response = await this._ows.updateOpenWaterTest(
            this.openWaterTest!.id,
            values.title,
            values.diveCertificate,
            values.content
        )

        if (!response.error) {
            this._sb.open("Openwaterproef is aangepast", "", snackbarConfig("success"))
            this._router.navigateByUrl("/open-water-tests")
        } else {
            this.ready = true
            this._sb.open(response.error.message, "", snackbarConfig("error"))
        }
    }

    onEditorChange(event: ChangeEvent) {
        try {
            this.addOpenWaterTestForm.value.content = event.editor.getData()
            this.emptyContent = !this.addOpenWaterTestForm.value.content
        } catch (e) { }
    }
}