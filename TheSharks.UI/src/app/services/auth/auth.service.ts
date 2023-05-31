import { Injectable } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { BehaviorSubject, firstValueFrom, map, Observable } from 'rxjs';
import { Jwt } from 'src/types/application/user/Jwt';
import { AuthRestService } from './auth-rest.service';
import { Claims } from 'src/types/application/role/Claims';
import { response } from '../response';
import { TokenDto } from 'src/types/dto/user/TokenDto';

@Injectable({
    providedIn: 'root'
})
export class AuthService {
    private _token$ = new BehaviorSubject<string>("")
    private _loggedIn$ = new BehaviorSubject<boolean>(false)
    loggedIn$ = this._loggedIn$.asObservable()
    token$ = this._token$.asObservable()

    constructor(private _rest: AuthRestService, private _jhs: JwtHelperService) {
    }

    authorizeClaims$(claims: Claims[]): Observable<boolean> {
        return this.token$.pipe(
            map((_) => this.authorizeClaims(claims))
        )
    }

    authorizeClaims(claims: Claims[]): boolean {
        const token = this.getDecodedToken()
        if (token === null) return false;

        let authorized = false;

        for (let claim of claims) {
            const i = Object.values(Claims).indexOf(claim as unknown as Claims);
            const key = Object.keys(Claims)[i];

            if (!(key in token)) {
                authorized = false;
                break;
            }
            authorized = true;
        }

        return authorized
    }

    validateToken() {
        const t = this._jhs.tokenGetter()

        if (t) {
            if (!this._jhs.isTokenExpired()) {
                this._token$.next(t)
                this._loggedIn$.next(true)
            } else {
                localStorage.removeItem("token")
                sessionStorage.removeItem("token")
            }
        }
    }

    getTokenAsync() {
        return firstValueFrom(this.token$);
    }

    getToken() {
        return this._token$.value
    }

    getDecodedToken(): Jwt {
        return this._jhs.decodeToken<Jwt>(this._token$.value)
    }

    getLoggedIn() {
        return firstValueFrom(this.loggedIn$);
    }

    async login(userName: string, password: string, persist: boolean) {
        const o$ = this._rest.postLogin({
            userName: userName,
            password: password,
            persist: persist
        })


        return response<TokenDto>(o$, (r) => {
            if (r.token !== null) {
                this._token$.next(r.token)
                this._loggedIn$.next(true)
                if (persist) {
                    localStorage.setItem("token", r.token)
                } else {
                    sessionStorage.setItem("token", r.token)
                }
            }
        })
    }

    logout() {
        this._loggedIn$.next(false);
        this._token$.next("");
        sessionStorage.removeItem("token")
        localStorage.removeItem("token")
    }

    async forgotPassword(email: string) {
        const o$ = this._rest.postForgotPassword({
            email: email
        })
        return response(o$)
    }
}
