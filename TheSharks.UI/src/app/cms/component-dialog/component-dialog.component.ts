import { Component, OnInit } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import { MatSelectionListChange } from '@angular/material/list';
import { Components } from 'src/types/application/cms/Components';

@Component({
    selector: 'app-component-dialog',
    templateUrl: './component-dialog.component.html',
    styleUrls: ['./component-dialog.component.scss']
})
export class ComponentDialogComponent implements OnInit {

    components = Object.values(Components)

    get type(): typeof Components {
        return Components
    }

    constructor(
        public ref: MatDialogRef<ComponentDialogComponent>,
    ) { }

    ngOnInit(): void {
    }

    onSelect(event: MatSelectionListChange) {
        this.ref.close(event.options[0].value)
    }
}
