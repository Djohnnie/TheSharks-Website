import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Breakpoints, BreakpointObserver } from '@angular/cdk/layout';
import { MatPaginator, MatPaginatorIntl, PageEvent } from '@angular/material/paginator';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { firstValueFrom, map, Observable, shareReplay, Subscription } from 'rxjs';
import { AuthService } from 'src/app/services/auth/auth.service';
import { MemberService } from 'src/app/services/member/member.service';
import { NewsItemService } from 'src/app/services/news-item/news-item.service';
import { UserService } from 'src/app/services/user/user.service';
import { snackbarConfig } from 'src/config/SnackbarConfig';
import { NewsItem } from 'src/types/application/news/NewsItem';
import { NewsItemList } from 'src/types/application/news/NewsItemList';
import { Claims } from 'src/types/application/role/Claims';
import { DeleteNewsItemDialogComponent } from '../delete-news-item-dialog/delete-news-item-dialog.component';

@Component({
    selector: 'app-news-item-list',
    templateUrl: './news-item-list.component.html',
    styleUrls: ['./news-item-list.component.scss']
})
export class NewsItemListComponent implements OnInit, OnDestroy {

    newsItemList: NewsItemList | undefined
    pageSize = 1
    image = "/assets/person-placeholder.jpg"

    currentUserId = ""
    private _authorId: string | undefined
    private _sub: Subscription | undefined

    authorized = false

    isHandset$: Observable<boolean> = this._bo.observe([Breakpoints.Small, Breakpoints.XSmall])
        .pipe(
            map(result => result.matches),
            shareReplay()
        );

    profilePictures = new Map<string, string>()

    constructor(
        private _nis: NewsItemService,
        private _route: ActivatedRoute,
        private _router: Router,
        private _us: UserService,
        private _as: AuthService,
        private _ms: MemberService,
        public dialog: MatDialog,
        private _bo: BreakpointObserver
    ) { }

    async ngOnInit() {
        this._sub = this._route.queryParams.subscribe(async (params) => {
            this._authorId = params["authorId"]
            await this.loadNewsItems(1)
        })

        const id = (await this._us.getUser()).response?.id
        if (id) {
            this.currentUserId = id
        }

        this.authorized = await this._as.authorizeClaims([Claims.ManageNewsItems])
    }

    ngOnDestroy(): void {
        this._sub!.unsubscribe()
    }

    private async loadProfilePictures() {
        if (!this.newsItemList) return;

        this.newsItemList.newsItems.forEach(async n => {
            const pic = await this._ms.getProfilePicture(n.authorId)
            if (pic.response) {
                this.profilePictures.set(n.authorId, "data:image/png;base64," + pic.response)
            } else {
                this.profilePictures.set(n.authorId, "/assets/person-placeholder.jpg")
            }
        })
    }

    async onPageChanged(event: number) {
        await this.loadNewsItems(event)
    }

    private async loadNewsItems(page: number) {
        if (this._authorId) {
            this.newsItemList = (await this._nis.getNewsItemsByAuthor(this._authorId, page, this.pageSize)).response
        } else {
            this.newsItemList = (await this._nis.getNewsItems(page, this.pageSize)).response
        }
        await this.loadProfilePictures()
    }

    async loadAuthorNewsItems(authorId: string) {
        this._router.navigate(
            [],
            {
                queryParams: { authorId: authorId },
                queryParamsHandling: "merge"
            }
        )
    }

    onEditNewsItem(id: string) {
        this._router.navigateByUrl("/edit-news-item/" + id)
    }

    async onDeleteNewsItem(id: string) {
        const ref = this.dialog.open(DeleteNewsItemDialogComponent, { data: id })

        if (await firstValueFrom(ref.afterClosed())) {
            await this.loadNewsItems(1)
        }
    }
}
