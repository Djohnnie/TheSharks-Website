import { Component, EventEmitter, Input, OnInit, Output, ViewChild } from '@angular/core';
import { MatTooltip } from '@angular/material/tooltip';

@Component({
    selector: 'app-action-menu-button',
    templateUrl: './action-menu-button.component.html',
    styleUrls: ['./action-menu-button.component.scss']
})
export class ActionMenuButtonComponent implements OnInit {

    @Input() text = ""
    @Input() color = "accent"
    @Input() disabled = false
    @Output() cclick = new EventEmitter()
    @Input() link: any | undefined

    constructor() { }

    ngOnInit(): void {
    }
}