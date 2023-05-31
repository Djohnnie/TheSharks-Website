import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';
import { LinkNode } from 'src/types/application/cms/LinkNode';
import { Page } from 'src/types/application/cms/Page';
import { CmsComponent } from 'src/types/application/cms/CmsComponent';
import { PageListItem } from 'src/types/application/cms/PageListItem';
import { UpdateMenuTreeDto } from 'src/types/dto/cms/UpdateMenuTreeDto';
import { response } from '../response';
import { CmsRestService } from './cms-rest.service';

@Injectable({
    providedIn: 'root'
})
export class CmsService {
    private _contentChanges$ = new Subject()

    constructor(private _rest: CmsRestService) { }

    contentChanges$() {
        return this._contentChanges$.asObservable()
    }

    contentChanged() {
        this._contentChanges$.next(true)
    }

    async getMenuTree() {
        const o$ = this._rest.getMenuTree()
        return response<LinkNode[]>(o$)
    }

    async getPage(link: string) {
        const o$ = this._rest.getPage(link)
        return response<Page>(o$)
    }

    async getPages() {
        const o$ = this._rest.getAllPages()
        return response<PageListItem[]>(o$)
    }

    async updateMenuTree(pages: PageListItem[]) {
        const dto: UpdateMenuTreeDto = {
            pages: [...pages]
        }
        const o$ = this._rest.putMenuTree(dto)
        return response(o$)
    }

    async updatePage(id: string, title: string, link: string, isOnlyAvailableForMembers: boolean, isDefaultPage: boolean, isDefaultPageForMembers: boolean, components: CmsComponent[], navBarPosition: number, navBarSubPosition: number) {
        const o$ = this._rest.putPage(id, {
            id: id,
            title: title,
            link: link,
            isOnlyAvailableForMembers: isOnlyAvailableForMembers,
            isDefaultPage: isDefaultPage,
            isDefaultPageForMembers: isDefaultPageForMembers,
            components: components,
            navBarPosition: navBarPosition,
            navBarSubPosition: navBarSubPosition
        })

        return response(o$)
    }

    async addPage(title: string, link: string) {
        const o$ = this._rest.postPage({
            title: title,
            link: link
        })

        return response(o$)
    }

    async removePage(id: string) {
        const o$ = this._rest.deletePage(id)
        return response(o$)
    }

    async getDefaultMembersPage() {
        const o$ = this._rest.getDefaultMembersPage()
        return response(o$);
    }

    async getDefaultPage() {
        const o$ = this._rest.getDefaultPage()
        return response(o$);
    }
}