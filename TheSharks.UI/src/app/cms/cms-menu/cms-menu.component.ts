import { Component, OnInit } from '@angular/core';

interface Link {
    label: string;
    link: string;
}

@Component({
    selector: 'app-cms-menu',
    templateUrl: './cms-menu.component.html',
    styleUrls: ['./cms-menu.component.scss']
})
export class CmsMenuComponent implements OnInit {

    links: Link[] = [
        {
            label: "Pagina's",
            link: "pages"
        },
        {
            label: "Menustructuur",
            link: "menu"
        }
    ]

    activeLink = this.links[0]

    constructor() { }

    ngOnInit(): void {
    }

}
