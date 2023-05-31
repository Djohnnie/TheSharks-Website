import { Component, Input, OnInit } from '@angular/core';
import { Activity } from 'src/types/application/activity/Activity';

@Component({
    selector: 'app-activity-details-only',
    templateUrl: './activity-details-only.component.html',
    styleUrls: ['./activity-details-only.component.scss']
})
export class ActivityDetailsOnlyComponent implements OnInit {

    @Input("activity") activity: Activity | undefined;
    @Input("showInfo") showInfo: boolean | undefined;

    constructor() { }

    ngOnInit(): void {
    }

}
