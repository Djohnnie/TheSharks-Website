import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { CmsService } from 'src/app/services/cms/cms.service';
import { UserService } from 'src/app/services/user/user.service';
import { Components } from 'src/types/application/cms/Components';
import { CmsComponent } from 'src/types/application/cms/CmsComponent';
import { Page } from 'src/types/application/cms/Page';

@Component({
    selector: 'app-page',
    templateUrl: './page.component.html',
    styleUrls: ['./page.component.scss']
})
export class PageComponent implements OnInit, OnDestroy {

    page: Page | undefined
    componentLeft: CmsComponent | undefined
    componentRight: CmsComponent | undefined
    private _sub: Subscription | undefined

    currentUserId = ""


    get componentTypes(): typeof Components {
        return Components
    }

    constructor(
        private _cmss: CmsService,
        private _route: ActivatedRoute,
        private _us: UserService,
        private _router: Router
    ) { }

    async ngOnInit() {
        this._sub = this._route.params.subscribe(async (params) => {
            this.componentLeft = undefined
            this.componentRight = undefined
            this.page = undefined

            const response = await this._cmss.getPage(params["link"])
            if (response.error === undefined) {
                const p = response.response!
                p.components = p?.components.sort((a, b) => a.position - b.position)

                //If page has no side components, stop
                switch (p.components.filter(c => c.position < 0).length) {
                    case 0: {
                        this.page = p;
                    } break
                    case 1: {
                        const c = p.components.shift()
                        if (c?.position === -1) this.componentLeft = c
                        else this.componentRight = c
                        this.page = p
                    } break;
                    case 2: {
                        this.componentRight = p.components.shift()
                        this.componentLeft = p.components.shift()
                        this.page = p
                    } break;
                }

            } else {
                this._router.navigateByUrl("**")
            }
        })        

        const id = (await this._us.getUser()).response?.id
        if (id) {
            this.currentUserId = id
        }
    }

    ngOnDestroy(): void {
        if (this._sub) this._sub.unsubscribe
    }
}
