import { Pipe, PipeTransform } from '@angular/core';
import { DomSanitizer } from '@angular/platform-browser';

@Pipe({
    name: 'skipSanitize'
})
export class SkipSanitizePipe implements PipeTransform {

    constructor(private _ds: DomSanitizer) {

    }

    transform(value: string, ...args: unknown[]): unknown {
        return this._ds.bypassSecurityTrustHtml(value)
    }

}
