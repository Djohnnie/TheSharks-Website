import { Component, OnInit } from '@angular/core';
import { Breakpoints, BreakpointObserver } from '@angular/cdk/layout';
import { firstValueFrom, map, Observable, shareReplay } from 'rxjs';
import { MatPaginatorIntl, PageEvent } from '@angular/material/paginator';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { GalleryService } from 'src/app/services/gallery/gallery.service';
import { GalleryList } from 'src/types/application/gallery/GalleryList';
import { GalleryListItem } from 'src/types/application/gallery/GalleryListItem';
import { RemoveGalleryComponent } from '../remove-gallery-dialog/remove-gallery.component';
import { AuthService } from 'src/app/services/auth/auth.service';
import { Claims } from 'src/types/application/role/Claims';

@Component({
    selector: 'app-gallery-list',
    templateUrl: './gallery-list.component.html',
    styleUrls: ['./gallery-list.component.scss']
})

export class GalleryListComponent implements OnInit {

    galleryList: GalleryList | undefined
    pageSize = 4
    placeHolder = "/assets/gallery-placeholder.png"
    authorized = false

    isHandset$: Observable<boolean> = this._bo.observe([Breakpoints.Small, Breakpoints.XSmall])
        .pipe(
            map(result => result.matches),
            shareReplay()
        );

    constructor(
        private _gs: GalleryService,
        private _as: AuthService,
        private _bo: BreakpointObserver) {
    }

    async ngOnInit() {
        this.loadGalleries()
        this.authorized = await this._as.authorizeClaims([Claims.ManageGalleries])
    }

    async onPageChanged(event: number) {
        this.loadGalleries(event)
    }

    getThumbnail(g: GalleryListItem) {
        if (g.urlFirstPicture) {
            return g.urlFirstPicture;
        } else {
            return this.placeHolder;
        }
    }

    async loadGalleries(page?: number) {
        let p = 1
        if (page) p = page
        this.galleryList = (await this._gs.getGalleries(p, this.pageSize)).response
    }
}