import { Component, OnDestroy, OnInit } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { ActivatedRoute, NavigationEnd, Router } from '@angular/router';
import { filter, map, Subscription } from 'rxjs';

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.scss'],
})
export class AppComponent implements OnInit, OnDestroy {
    title = "The Sharks"
    noLandingScreen = false
    private _sub: Subscription | undefined

    constructor(private router: Router, private titleService: Title) {
    }

    ngOnDestroy(): void {
        this._sub?.unsubscribe()
    }

    //Apply title to browser
    ngOnInit() {
        this._sub = this.router.events.pipe(
            filter((event) => event instanceof NavigationEnd),
            map(() => {
                let route: ActivatedRoute = this.router.routerState.root;
                let routeTitle = '';
                while (route!.firstChild) {
                    route = route.firstChild;
                }
                if (route.snapshot.data['title']) {
                    routeTitle = route!.snapshot.data['title'];
                }
                return routeTitle;
            })
        ).subscribe((title: string) => {
            if (title) {
                this.titleService.setTitle(`The Sharks - ${title}`);
            }
        });

        this.landingScreenOptions()
    }

    landingScreenOptions() {
        const check = localStorage.getItem("nolandingscreen")
        if (check) this.noLandingScreen = Boolean(localStorage.getItem("nolandingscreen") === "true")
    }
}
