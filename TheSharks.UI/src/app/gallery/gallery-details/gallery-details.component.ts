import { GalleryService } from "src/app/services/gallery/gallery.service"
import { GalleryDetail } from "src/types/application/gallery/GalleryDetail"
import { RemoveGalleryComponent } from '../remove-gallery-dialog/remove-gallery.component';
import { Component, OnInit } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { ActivatedRoute } from '@angular/router';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { UploadPictureComponent } from "../upload-picture-dialog/upload-picture.component";
import { GalleryPicture } from "src/types/application/gallery/GalleryPicture";
import { MatSnackBar } from '@angular/material/snack-bar';
import { snackbarConfig } from 'src/config/SnackbarConfig';
import { RemovePictureModel } from "src/types/dto/gallery/RemovePictureModel";
import { AuthService } from "src/app/services/auth/auth.service";
import { Claims } from "src/types/application/role/Claims";

@Component({
    selector: 'app-gallery-details',
    templateUrl: './gallery-details.component.html',
    styleUrls: ['./gallery-details.component.scss']
})

export class GalleryDetailsComponent implements OnInit {
    gallery: GalleryDetail | undefined
    toRemovePictures: RemovePictureModel[] = []
    selectMode: boolean = false

    currentPicture: GalleryPicture | undefined
    currentIndex: number = -1

    authorized = false

    constructor(
        private _sb: MatSnackBar,
        private _gs: GalleryService,
        private _route: ActivatedRoute,
        private _as: AuthService,
        private dialog: MatDialog
    ) {
    }

    async ngOnInit() {
        const params = await firstValueFrom(this._route.params)

        this.gallery = (await this._gs.getGallery(params["id"])).response
        this.authorized = this._as.authorizeClaims([Claims.ManageGalleries])
    }

    async openDialog() {
        const dialogConfig = new MatDialogConfig();
        dialogConfig.data = {
            id: this.gallery!.id,
            name: this.gallery!.name
        }

        this.dialog.open(RemoveGalleryComponent, dialogConfig);
    }

    async openPictureDialog() {
        const dialogConfig = new MatDialogConfig();
        dialogConfig.data = {
            id: this.gallery!.id,
        }

        const dialog = this.dialog.open(UploadPictureComponent, dialogConfig);
        const result = await firstValueFrom(dialog.afterClosed())
        if (result !== undefined) this.gallery = (await this._gs.getGallery(this.gallery!.id)).response
    }

    openPicture(index: number) {
        this.currentIndex = index
        this.currentPicture = this.gallery?.pictures[index]
    }

    closePicture() {
        this.currentPicture = undefined
    }

    toggleAppendList(name: string, id: string) {
        if (this.toRemovePictures.find(_ => _.name == name) != undefined) {
            this.toRemovePictures = this.toRemovePictures.filter(_ => _.name != name)
        } else {
            this.toRemovePictures.push({ id: id, name: name })
        }
    }

    toggleSelectMode() {
        this.selectMode = !this.selectMode
        this.toRemovePictures = []
    }

    async removePictures() {
        const response = await this._gs.removePicturesFromGallery(this.gallery!.id, this.toRemovePictures)

        if (response.error === undefined) {
            this._sb.open("Foto's verwijderd", "", snackbarConfig("success"))
            await this.ngOnInit()
        } else {
            this._sb.open(response.error.message, "", snackbarConfig("error"))
        }
    }
}