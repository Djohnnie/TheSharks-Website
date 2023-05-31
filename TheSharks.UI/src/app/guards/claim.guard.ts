import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Claims } from 'src/types/application/role/Claims';
import { AuthService } from '../services/auth/auth.service';
import { ActivityService } from '../services/activity/activity.service';
import { UserService } from '../services/user/user.service';

@Injectable({
    providedIn: 'root'
})
export class ClaimGuard implements CanActivate {

    constructor(
        private _auth: AuthService, 
        private _as: ActivityService,   
        private _us: UserService) { }

    async canActivate(
        route: ActivatedRouteSnapshot,
        state: RouterStateSnapshot
    ): Promise<boolean | UrlTree> {
        const claims = route.data["claims"] as Claims[]
        const activityId = route.params["id"]
        const activityType = route.params["type"]

        const authorized = await this._auth.authorizeClaims(claims)

        if(!activityId || !activityType)
        {
            return authorized;
        }

        const activity = await this._as.getActivity(activityId, activityType)
        const isResponsible = activity.response?.responsibleId === (await this._us.getUser()).response?.id

        return authorized || isResponsible
    }
}