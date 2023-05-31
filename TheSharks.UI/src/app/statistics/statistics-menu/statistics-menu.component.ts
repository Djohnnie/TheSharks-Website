import { Component, OnInit } from '@angular/core';

interface Link {
    label: string;
    link: string;
}

@Component({
    selector: 'app-statistics-menu',
    templateUrl: './statistics-menu.component.html',
    styleUrls: ['./statistics-menu.component.scss']
})
export class StatisticsMenuComponent implements OnInit {

    links: Link[] = [
        {
            label: "Overzicht",
            link: "overview"
        },
        {
            label: "Vandaag",
            link: "today"
        },
        {
            label: "Deze week",
            link: "week"
        },
        {
            label: "Deze maand",
            link: "month"
        },
        {
            label: "Dit jaar",
            link: "year"
        }
    ]

    activeLink = this.links[0]
    constructor() { }

    ngOnInit(): void { }

}