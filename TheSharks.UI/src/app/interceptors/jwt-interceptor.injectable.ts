import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable, tap } from "rxjs";
import { AuthService } from "../services/auth/auth.service";

@Injectable()
export class JwtInterceptor implements HttpInterceptor {

    constructor(private _as: AuthService) { }

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        const token = this._as.getToken()

        let sessionId = sessionStorage.getItem("SessionId")
        if( !sessionId )
        {
            sessionId = crypto.randomUUID();
            sessionStorage.setItem("SessionId", sessionId);
        }

        let isApp = localStorage.getItem("IsApp") === "true" ? "True" : "False"

        const newReq = req.clone({
            headers: req.headers
                .set('Authorization', 'Bearer ' + token)
                .set('TheSharksSession', sessionId)
                .set('TheSharksIsApp', isApp)
        })

        return next.handle(newReq)
    }
}