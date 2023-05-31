import { Component, OnInit } from '@angular/core';
import { UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { snackbarConfig } from 'src/config/SnackbarConfig';
import { GalleryService } from 'src/app/services/gallery/gallery.service';

@Component({
    selector: 'app-add-gallery',
    templateUrl: './add-gallery.component.html',
    styleUrls: ['./add-gallery.component.scss']
})

export class AddGalleryComponent implements OnInit {

    galleryForm = new UntypedFormGroup({
        name: new UntypedFormControl("", [Validators.required])
    })

    constructor(
        private _gs: GalleryService,
        private _sb: MatSnackBar,
        private _router: Router
    ) { }

    async ngOnInit() {
    }

    async onSubmit() {
        const response = await this._gs.addGallery(
            this.galleryForm.value.name,
        )

        if (response.error === undefined) {
            this._sb.open("Gallerij aangemaakt", "", snackbarConfig("success"))
            this._router.navigateByUrl("/galleries")
        } else {
            this._sb.open(response.error.message, "", snackbarConfig("error"))
        }
    }
}