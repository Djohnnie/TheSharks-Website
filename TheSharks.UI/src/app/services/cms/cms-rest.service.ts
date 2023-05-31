import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs';
import { environment } from 'src/environments/environment';
import { LinkNode } from 'src/types/application/cms/LinkNode';
import { Page } from 'src/types/application/cms/Page';
import { PageListItem } from 'src/types/application/cms/PageListItem';
import { AddPageDto } from 'src/types/dto/cms/AddPageDto';
import { MenuTreeDto } from 'src/types/dto/cms/MenuTreeDto';
import { UpdateMenuTreeDto } from 'src/types/dto/cms/UpdateMenuTreeDto';
import { UpdatePageDto } from 'src/types/dto/cms/UpdatePageDto';
import { GetDefaultPageDto } from 'src/types/dto/cms/GetDefaultPageDto';

@Injectable({
    providedIn: 'root'
})
export class CmsRestService {

    constructor(private _http: HttpClient) { }

    getMenuTree() {
        return this._http.get<MenuTreeDto>(`${environment.baseUrl}/api/Page/menuTree`).pipe(
            map<MenuTreeDto, LinkNode[]>(tree => {
                return tree.tree
            })
        )
    }

    getPage(link: string) {
        return this._http.get<Page>(`${environment.baseUrl}/api/Page/${link}`)
    }

    putMenuTree(dto: UpdateMenuTreeDto) {
        return this._http.put(`${environment.baseUrl}/api/Page/menuTree`, dto)
    }

    getAllPages() {
        return this._http.get(`${environment.baseUrl}/api/Page`).pipe(
            map<any, PageListItem[]>(value => {
                return value.pages
            })
        )
    }

    putPage(id: string, dto: UpdatePageDto) {
        return this._http.put(`${environment.baseUrl}/api/Page/${id}`, dto)
    }

    postPage(dto: AddPageDto) {
        return this._http.post(`${environment.baseUrl}/api/Page`, { ...dto, components: [] })
    }

    deletePage(id: string) {
        return this._http.delete(`${environment.baseUrl}/api/Page/${id}`)
    }

    getDefaultMembersPage() {
        return this._http.get<GetDefaultPageDto>(`${environment.baseUrl}/api/Page/members/default`).pipe(
            map<GetDefaultPageDto, string>(value => {
                return value.link
            })
        )
    }

    getDefaultPage() {
        return this._http.get<GetDefaultPageDto>(`${environment.baseUrl}/api/Page/default`).pipe(
            map<GetDefaultPageDto, string>(value => {
                return value.link
            })
        )
    }
}