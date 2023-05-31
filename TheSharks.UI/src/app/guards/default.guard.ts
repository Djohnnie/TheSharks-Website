import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { filter, map, shareReplay, withLatestFrom } from 'rxjs/operators';
import { AuthService } from 'src/app/services/auth/auth.service';
import { CmsService } from 'src/app/services/cms/cms.service';
import { Router } from '@angular/router';
import { Claims } from 'src/types/application/role/Claims';

@Injectable({
    providedIn: 'root'
})

export class DefaultGuard implements CanActivate {

    constructor(
        private _as: AuthService,
        private _cmss: CmsService,
        private _router: Router) 
    { }

    async canActivate() : Promise<boolean> {

        if( await this._as.getLoggedIn())
        {
            const defaultMemberPage = await this._cmss.getDefaultMembersPage();
            this._router.navigateByUrl(`/${defaultMemberPage.response}`)
        }
        else
        {
            const defaultPage = await this._cmss.getDefaultPage();
            this._router.navigateByUrl(`/${defaultPage.response}`)
        }

        return false;
    }

}