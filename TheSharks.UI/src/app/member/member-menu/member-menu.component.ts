import { Component, OnInit } from '@angular/core';

interface Link {
    label: string;
    link: string;
}

@Component({
    selector: 'app-member-menu',
    templateUrl: './member-menu.component.html',
    styleUrls: ['./member-menu.component.scss']
})
export class MemberMenuComponent implements OnInit {

    links: Link[] = [
        {
            label: "Leden",
            link: "memberlist"
        },
        {
            label: "Groepen",
            link: "grouplist"
        }
    ]

    activeLink = this.links[0]
    constructor() { }

    ngOnInit(): void {
    }

}
