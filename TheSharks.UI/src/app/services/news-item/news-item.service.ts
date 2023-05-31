import { Injectable } from '@angular/core';
import { AddNewsItemDto } from 'src/types/dto/news/AddNewsItemDto';
import { UpdateNewsItemDto } from 'src/types/dto/news/UpdateNewsItemDto';
import { NewsItem } from 'src/types/application/news/NewsItem';
import { NewsItemRestService } from './news-item-rest.service';
import { NewsItemList } from 'src/types/application/news/NewsItemList';
import { response } from '../response';

@Injectable({
    providedIn: 'root'
})
export class NewsItemService {

    constructor(private _rest: NewsItemRestService) { }

    async getNewsItem(id: string) {
        const o$ = this._rest.getNewsItem(id)
        return response<NewsItem>(o$)
    }

    async addNewsItem(authorId: string, title: string, content: string, membersOnly: boolean) {
        const dto: AddNewsItemDto = {
            author: authorId,
            title: title,
            content: content,
            membersOnly: membersOnly
        }

        const o$ = this._rest.postNewsItem(dto)
        return response(o$)
    }

    async updateNewsItem(id: string, title: string, content: string, originalPublishDate: Date, membersOnly: boolean) {
        const dto: UpdateNewsItemDto = {
            id: id,
            title: title,
            content: content,
            originalPublishDate: originalPublishDate,
            membersOnly: membersOnly
        }

        const o$ = this._rest.putNewsItem(dto)
        return response(o$)
    }

    async getNewsItems(page: number, size: number) {
        const o$ = this._rest.getNewsItemsByPagination(page, size)
        return response<NewsItemList>(o$)
    }

    async getNewsItemsByAuthor(authorId: string, page: number, size: number) {
        const o$ = this._rest.getNewsItemsByAuthorAndPagination(authorId, page, size)
        return response<NewsItemList>(o$)
    }

    async deleteNewsItem(id: string) {
        const o$ = this._rest.deleteNewsItem(id)
        return response(o$)
    }
}