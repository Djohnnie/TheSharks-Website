import { Component, OnInit } from '@angular/core';
import { Breakpoints, BreakpointObserver } from '@angular/cdk/layout';
import { CmsService } from 'src/app/services/cms/cms.service';
import { map, Observable, shareReplay } from 'rxjs';
import { CdkDrag, CdkDragDrop, moveItemInArray, transferArrayItem } from '@angular/cdk/drag-drop';
import { PageListItem } from 'src/types/application/cms/PageListItem';
import { MatSnackBar } from '@angular/material/snack-bar';
import { snackbarConfig } from 'src/config/SnackbarConfig';

@Component({
    selector: 'app-manage-menu-tree',
    templateUrl: './manage-menu-tree.component.html',
    styleUrls: ['./manage-menu-tree.component.scss']
})
export class ManageMenuTreeComponent implements OnInit {

    menuTree: PageListItem[][] | undefined
    showEmptyList = false

    isHandset$: Observable<boolean> = this._bo.observe([Breakpoints.Small, Breakpoints.XSmall])
        .pipe(
            map(result => result.matches),
            shareReplay()
        );

    constructor(
        private _cmss: CmsService, 
        private _sb: MatSnackBar,
        private _bo: BreakpointObserver
        ) { }

    async ngOnInit() {
        const response = await this._cmss.getPages()

        if (response.response) {
            const pages = response.response
            this.menuTree = this.groupBy(pages!, "navBarPosition")
            this.menuTree?.push([])
        }
    }

    private groupBy = function (xs: any[], key: string) {
        return xs.reduce(function (rv, x) {
            (rv[x[key]] = rv[x[key]] || []).push(x);
            return rv;
        }, []);
    };

    async onSave() {
        const pages = this.menuTree!.flat()
        const response = await this._cmss.updateMenuTree(pages)

        if (response.error?.code === 200) {
            this._sb.open("Menu gewijzigd", "", snackbarConfig("success"))
            this._cmss.contentChanged()
        }
    }

    drop(event: CdkDragDrop<PageListItem[]>) {
        if (event.previousContainer === event.container) {
            moveItemInArray(event.container.data, event.previousIndex, event.currentIndex);
        } else {
            transferArrayItem(
                event.previousContainer.data,
                event.container.data,
                event.previousIndex,
                event.currentIndex,
            );
        }

        this.updateNavBarPositions()
        this.ensureOneEmptyList()
    }

    private updateNavBarPositions() {
        if (this.menuTree === undefined) return;

        this.menuTree = this.menuTree.filter(b => b.length > 0)

        for (let i = 0; i < this.menuTree.length; i++) {
            for (let j = 0; j < this.menuTree[i].length; j++) {
                this.menuTree[i][j].navBarPosition = i
                this.menuTree[i][j].navBarSubPosition = j
            }
        }
    }

    private ensureOneEmptyList() {
        if (!this.menuTree) return;

        this.menuTree = this.menuTree.filter(b => b.length !== 0)
        this.menuTree.push([])
    }

    noNewListPredicate(item: CdkDrag<PageListItem>) {
        return item.data.navBarSubPosition !== 0
    }

    toggleEmptyList() {
        this.showEmptyList = !this.showEmptyList
    }
}
