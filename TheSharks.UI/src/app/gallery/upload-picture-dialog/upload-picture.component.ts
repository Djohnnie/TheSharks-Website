import { Component, Directive, EventEmitter, HostListener, Inject, OnInit, Output } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from "@angular/material/dialog";
import { MatSnackBar } from '@angular/material/snack-bar';
import { snackbarConfig } from 'src/config/SnackbarConfig';
import { GalleryService } from "src/app/services/gallery/gallery.service";
import { size_10MB, size_2MB } from 'src/util/constants';

interface ImagePreview {
    content: string;
    file: File
}

@Component({
    selector: 'app-upload-picture-dialog',
    templateUrl: './upload-picture.component.html',
    styleUrls: ['./upload-picture.component.scss']
})

export class UploadPictureComponent implements OnInit {
    files: ImagePreview[] = []
    id: string

    constructor(
        private _gs: GalleryService,
        private _sb: MatSnackBar,
        private dialogRef: MatDialogRef<UploadPictureComponent>,
        @Inject(MAT_DIALOG_DATA) data) {

        this.id = data.id;
    }

    async ngOnInit() {
    }

    filesDropped(files) {
        this.addPictures(files)
    }

    fileBrowseHandler($event) {
        this.addPictures($event.target.files)
    }

    addPictures(files: Array<File>) {
        for (let i = 0; i < files.length; i++) {
            if (files[i].size > size_10MB) {
                this._sb.open("Het gekozen bestand is te groot", "", snackbarConfig("error"))
                break
            }

            const reader = new FileReader()
            let preview = ""
            reader.onload = () => {
                preview = reader.result!.toString()

                this.files.push({
                    content: preview,
                    file: files[i]
                })
            }
            reader.readAsDataURL(files[i])
        }
    }

    deleteFile(index: number) {
        this.files = this.files.filter((f, i) => i !== index);
    }

    async save() {
        const response = await this._gs.addPicturesToGallery(this.id, this.files.map(_ => _.file))

        if (response.error === undefined) {
            this._sb.open("Foto's geüpload!", "", snackbarConfig("success"))
            this.dialogRef.close("ok");
        } else {
            this._sb.open(response.error.message, "", snackbarConfig("error"))
        }
    }

    close() {
        this.dialogRef.close()
    }
}

@Directive({ selector: "[picturednd]" })
export class DropDirective {

    @Output() droppedFiles = new EventEmitter<any>();

    constructor() { }

    @HostListener("dragover", ["$event"]) public onDragOver(evt: DragEvent) {
        evt.preventDefault()
        evt.stopPropagation()
    }

    @HostListener("dragleave", ["$event"]) public onDragLeave(evt: DragEvent) {
        evt.preventDefault();
        evt.stopPropagation();
    }

    @HostListener('drop', ['$event']) public onDrop(evt: DragEvent) {
        evt.preventDefault()
        evt.stopPropagation()

        const files = evt.dataTransfer!.files
        if (files.length > 0) this.droppedFiles.emit(files)
    }
}