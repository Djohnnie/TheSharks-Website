import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from "@angular/material/dialog";
import { MatSnackBar } from '@angular/material/snack-bar';
import { snackbarConfig } from 'src/config/SnackbarConfig';
import { Router } from '@angular/router';
import { GalleryService } from "src/app/services/gallery/gallery.service";

@Component({
  selector: 'app-remove-gallery-dialog',
  templateUrl: './remove-gallery.component.html',
  styleUrls: ['./remove-gallery.component.scss']
})

export class RemoveGalleryComponent implements OnInit {

  id: string;
  name: string;

  constructor(
    private _gs: GalleryService,
    private _sb: MatSnackBar,
    private _router: Router,
    private dialogRef: MatDialogRef<RemoveGalleryComponent>,
    @Inject(MAT_DIALOG_DATA) data) {

    this.id = data.id;
    this.name = data.name;
  }

  async ngOnInit() {
  }

  async save() {
    const response = await this._gs.removeGallery(this.id)

    if (response.error === undefined) {
      this._sb.open("Gallerij verwijderd", "", snackbarConfig("success"))
      this.dialogRef.close();
      this._router.navigateByUrl("/galleries")
    } else {
      this._sb.open(response.error.message, "", snackbarConfig("error"))
    }
  }

  async close() {
    this.dialogRef.close();
  }
}