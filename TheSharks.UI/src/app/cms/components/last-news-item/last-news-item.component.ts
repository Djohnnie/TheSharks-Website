import { Component, OnInit } from '@angular/core';
import { MemberService } from 'src/app/services/member/member.service';
import { NewsItemService } from 'src/app/services/news-item/news-item.service';
import { NewsItem } from 'src/types/application/news/NewsItem';

interface NewsItemPicture extends NewsItem {
    picture: string;
}

@Component({
    selector: 'app-last-news-item',
    templateUrl: './last-news-item.component.html',
    styleUrls: ['./last-news-item.component.scss']
})
export class LastNewsItemComponent implements OnInit {

    newsItem: NewsItemPicture | undefined

    constructor(private _nis: NewsItemService, private _ms: MemberService) { }

    async ngOnInit() {
        const response = await this._nis.getNewsItems(1, 1)
        if (!response.response) return;

        this.newsItem = { ...response.response.newsItems[0], picture: "/assets/person-placeholder.jpg" }

        this.loadProfilePicture()
    }

    private async loadProfilePicture() {
        if (!this.newsItem) return;

        const pic = await this._ms.getProfilePicture(this.newsItem.authorId)

        if (pic.response) {
            this.newsItem.picture = "data:image/png;base64," + pic.response;
        } else {
            this.newsItem.picture = "/assets/person-placeholder.jpg"
        }
    }
}
