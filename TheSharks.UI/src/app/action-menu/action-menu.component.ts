import { BreakpointObserver, Breakpoints } from '@angular/cdk/layout';
import { Component, EventEmitter, OnDestroy, OnInit, Output } from '@angular/core';
import { Observable, map, shareReplay, Subscription } from 'rxjs';
import { ActionMenuAnimations } from './action-menu.animations';

@Component({
    selector: 'app-action-menu',
    templateUrl: './action-menu.component.html',
    styleUrls: ['./action-menu.component.scss'],
    animations: ActionMenuAnimations
})
export class ActionMenuComponent implements OnInit, OnDestroy {

    isOpen = false
    @Output() open = new EventEmitter<boolean>()

    private _sub: Subscription | undefined

    isHandset$: Observable<boolean> = this._bo.observe([Breakpoints.Small, Breakpoints.XSmall])
        .pipe(
            map(result => result.matches),
            shareReplay()
        );

    constructor(private _bo: BreakpointObserver) {
        this._sub = this.isHandset$.subscribe(m => this.isOpen = !m)
    }

    ngOnDestroy(): void {
        this._sub!.unsubscribe()
    }

    ngOnInit(): void {
    }

    toggleMenu() {
        this.isOpen = !this.isOpen
        this.open.emit(this.isOpen)
    }
}
