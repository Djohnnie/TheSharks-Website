import { Component, Inject, OnDestroy, OnInit } from '@angular/core';
import { UntypedFormControl } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatSelectionListChange } from '@angular/material/list';
import { Subscription } from 'rxjs';
import { MemberListItem } from 'src/types/application/member/MemberListItem';

@Component({
    selector: 'app-member-list-dialog',
    templateUrl: './member-list-dialog.component.html',
    styleUrls: ['./member-list-dialog.component.scss']
})
export class MemberListDialogComponent implements OnInit, OnDestroy {

    members: MemberListItem[] | undefined
    filteredList: MemberListItem[] | undefined

    search = new UntypedFormControl("")

    private _sub: Subscription | undefined

    constructor(
        public ref: MatDialogRef<MemberListDialogComponent>,
        @Inject(MAT_DIALOG_DATA) private data: MemberListItem[]
    ) { }

    async ngOnInit() {
        this.members = this.data
        this.filteredList = this.data
        this.enableFilter()
    }

    ngOnDestroy(): void {
        this._sub?.unsubscribe()
    }

    enableFilter() {
        this._sub = this.search.valueChanges.subscribe((v: string) => {
            const f = v.trim().toLowerCase()
            this.filteredList = this.members!.filter((m) => {
                const fn = m.firstName.trim().toLowerCase()
                const ln = m.lastName.trim().toLowerCase()

                return fn.includes(f) || ln.includes(f)
            })
        })
    }

    onSelect(event: MatSelectionListChange) {
        this.ref.close(event.options[0].value)
    }
}
