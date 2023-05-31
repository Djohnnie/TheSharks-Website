import { Component, EventEmitter, Input, OnDestroy, OnInit, Output } from '@angular/core';
import { AbstractControl, UntypedFormArray, UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Subscription } from 'rxjs';
import { snackbarConfig } from 'src/config/SnackbarConfig';
import { size_1MB, size_2MB, size_500KB } from 'src/util/constants';

interface DisplayComponentContent {
    valid?: boolean;
    title: string;
    items: DisplayItem[]
}

interface DisplayItem {
    image: string;
    text: string;
    email: string;
}

@Component({
    selector: 'app-display',
    templateUrl: './display.component.html',
    styleUrls: ['./display.component.scss']
})
export class DisplayComponent implements OnInit, OnDestroy {

    displayForm = new UntypedFormGroup({
        title: new UntypedFormControl(""),
        items: new UntypedFormArray([])
    })

    @Output() change = new EventEmitter<DisplayComponentContent>()
    @Input() contentString: string | undefined
    content: DisplayComponentContent | undefined
    @Input() editMode = false

    private _sub: Subscription | undefined

    get items() {
        return this.displayForm.get("items") as UntypedFormArray
    }

    constructor(private _sb: MatSnackBar) { }

    ngOnDestroy() {
        this._sub?.unsubscribe()
    }

    ngOnInit() {
        this._sub = this.displayForm.valueChanges.subscribe(v => {
            this.change.emit({
                valid: this.displayForm.valid,
                title: v.title,
                items: v.items
            })
        })

        this.loadContent()
    }

    loadContent() {
        if (!this.contentString) return;
        this.content = JSON.parse(this.contentString)
        this.displayForm.get("title")?.setValue(this.content!.title, { emitEvent: false })
        for (let item of this.content!.items) {
            this.items.push(new UntypedFormGroup({
                image: new UntypedFormControl(item.image),
                text: new UntypedFormControl(item.text),
                email: new UntypedFormControl(item.email)
            }))
        }
    }

    addDisplayItem() {
        this.items.push(new UntypedFormGroup({
            image: new UntypedFormControl(null),
            text: new UntypedFormControl(""),
            email: new UntypedFormControl(""),
        }))
    }

    deleteDisplayItem(index: number) {
        this.items.removeAt(index)
    }

    getImageValue(control: AbstractControl): string | null {
        const group = control as UntypedFormGroup
        return group.value.image
    }

    onImageSelect(fileUploader: HTMLInputElement, control: AbstractControl) {
        const group = control as UntypedFormGroup

        //Preview image and save change to form
        if (fileUploader.files?.length !== undefined) {
            if (fileUploader.files?.length > 0) {
                const file = fileUploader.files[0];

                if (file.size > size_2MB) {
                    this._sb.open("Het gekozen bestand is groter dan 2MB", "", snackbarConfig("error"))
                } else {

                    const reader = new FileReader();
                    reader.onload = () => {
                        group.patchValue({ image: reader.result!.toString() });
                        group.updateValueAndValidity();
                    }

                    reader.readAsDataURL(file)
                }
            }
        }
    }
}
