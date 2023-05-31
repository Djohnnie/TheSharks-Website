import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs';
import { environment } from 'src/environments/environment';
import { AddNewsItemDto } from 'src/types/dto/news/AddNewsItemDto';
import { NewsItemDto } from 'src/types/dto/news/NewsItemDto';
import { NewsItemListDto } from 'src/types/dto/news/NewsItemListDto';
import { UpdateNewsItemDto } from 'src/types/dto/news/UpdateNewsItemDto';
import { NewsItem } from 'src/types/application/news/NewsItem';
import { NewsItemList } from 'src/types/application/news/NewsItemList';

@Injectable({
    providedIn: 'root'
})
export class NewsItemRestService {

    constructor(private _http: HttpClient) { }

    getNewsItem(id: string) {
        return this._http.get<NewsItemDto>(`${environment.baseUrl}/api/NewsItem/${id}`).pipe(
            map<NewsItemDto, NewsItem>((v) => {
                return { ...v, publishDate: new Date(v.publishDate), membersOnly: v.membersOnly }
            })
        )
    }

    postNewsItem(dto: AddNewsItemDto) {
        return this._http.post(`${environment.baseUrl}/api/NewsItem`, dto)
    }

    putNewsItem(dto: UpdateNewsItemDto) {
        return this._http.put(`${environment.baseUrl}/api/NewsItem`, dto)
    }

    getNewsItemsByPagination(page: number, size: number) {
        return this._http.get<NewsItemListDto>(`${environment.baseUrl}/api/NewsItem?Page=${page}&RecordsPerPage=${size}`).pipe(
            map<NewsItemListDto, NewsItemList>((v) => {
                return {
                    totalRecords: v.totalRecords, newsItems: v.newsItems.map((n) => {
                        return { ...n, publishDate: new Date(n.publishDate) }
                    })
                }
            })
        )
    }

    getNewsItemsByAuthorAndPagination(authorId: string, page: number, size: number) {
        return this._http.get<NewsItemListDto>(`${environment.baseUrl}/api/Member/${authorId}/newsitems?Page=${page}&RecordsPerPage=${size}`).pipe(
            map<NewsItemListDto, NewsItemList>((v) => {
                return {
                    totalRecords: v.totalRecords, newsItems: v.newsItems.map((n) => {
                        return { ...n, publishDate: new Date(n.publishDate) }
                    })
                }
            })
        )
    }

    deleteNewsItem(id: string) {
        return this._http.delete(`${environment.baseUrl}/api/NewsItem/${id}`)
    }
}
