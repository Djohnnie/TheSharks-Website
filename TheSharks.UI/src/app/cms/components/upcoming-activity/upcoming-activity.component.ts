import { Component, Input, OnInit } from '@angular/core';
import { ActivityService } from 'src/app/services/activity/activity.service';
import { UserService } from 'src/app/services/user/user.service';
import { Activity } from 'src/types/application/activity/Activity';
import { ActivityListItem } from 'src/types/application/activity/ActivityListItem';

@Component({
    selector: 'app-upcoming-activity',
    templateUrl: './upcoming-activity.component.html',
    styleUrls: ['./upcoming-activity.component.scss']
})
export class UpcomingActivityComponent implements OnInit {

    @Input() editMode: boolean = false

    activity: ActivityListItem | undefined

    loading = true

    constructor(
        private _as: ActivityService,
        private _us: UserService
    ) { }

    async ngOnInit() {
        const user = await this._us.getUser()

        if (user.response) {
            const response = await this._as.getUpcomingActivity(user.response.id)
            if (response.response === null) {
                this.activity = undefined
            } else {
                this.activity = response.response
            }
        } else {
            this.activity = undefined
        }
        this.loading = false
    }

}
