import { BreakpointObserver, Breakpoints } from '@angular/cdk/layout';
import { Component, EventEmitter, Input, OnChanges, OnInit, Output, SimpleChanges } from '@angular/core';
import { Observable, map, shareReplay } from 'rxjs';

@Component({
    selector: 'app-paginator',
    templateUrl: './paginator.component.html',
    styleUrls: ['./paginator.component.scss']
})
export class PaginatorComponent implements OnInit, OnChanges {

    @Input() pageSize: number | undefined
    @Input() length: number | undefined
    @Output() page: EventEmitter<number> = new EventEmitter<number>()

    pages: number | undefined

    isHandset$: Observable<boolean> = this._bo.observe([Breakpoints.Small, Breakpoints.XSmall])
        .pipe(
            map(result => result.matches),
            shareReplay()
        );

    currentPage = 1

    constructor(
        private _bo: BreakpointObserver
    ) {

    }
    ngOnChanges(changes: SimpleChanges): void {
        this.calculatePages()
        this.currentPage = 1
    }

    calculatePages() {
        this.pages = Math.ceil(this.length! / this.pageSize!)
    }

    ngOnInit(): void {
        this.calculatePages()
    }

    onPageChanged(delta: number) {
        this.currentPage += delta
        this.page.emit(this.currentPage)
    }
}
