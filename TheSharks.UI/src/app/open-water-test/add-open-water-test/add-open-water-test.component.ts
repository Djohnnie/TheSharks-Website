import { Component, OnInit } from '@angular/core';
import { UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { MemberService } from 'src/app/services/member/member.service';
import { RoleService } from 'src/app/services/role/role.service';
import { OpenWaterTestService } from 'src/app/services/open-water-test/open-water-test.service';
import { MemberItem } from 'src/types/application/member/MemberItem';
import { DiveCertificateDto } from 'src/types/dto/role/DiveCertificateDto';
import { snackbarConfig } from 'src/config/SnackbarConfig';
import * as ClassicEditor from 'ckeditor5/build/ckeditor';
import { ChangeEvent } from '@ckeditor/ckeditor5-angular';

@Component({
    selector: 'add-open-water-test',
    templateUrl: './add-open-water-test.component.html',
    styleUrls: ['./add-open-water-test.component.scss']
})
export class AddOpenWaterTestComponent implements OnInit {
    
    ready = false
    member: MemberItem | undefined

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
        private _ms: MemberService, 
        private _rs: RoleService,
        private _sb: MatSnackBar,
        private _router: Router) {
    }

    async ngOnInit() {
        this.diveCertificatesList = (await this._rs.getRoles()).response
        this.ready = true
    }

    async onSubmit() {
        this.ready = false
        const values = this.addOpenWaterTestForm.value

        const response = await this._ows.addOpenWaterTest(
            values.title,
            values.diveCertificate,
            values.content
        )

        if (!response.error) {
            this._sb.open("Openwaterproef is toegevoegd", "", snackbarConfig("success"))
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