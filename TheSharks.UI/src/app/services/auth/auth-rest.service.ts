import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { ForgotPasswordDto } from 'src/types/dto/user/ForgotPassword';
import { LoginDto } from 'src/types/dto/user/LoginDto';
import { TokenDto } from 'src/types/dto/user/TokenDto';

@Injectable({
    providedIn: 'root'
})
export class AuthRestService {

    constructor(private _http: HttpClient) {

    }

    postLogin(dto: LoginDto) {
        return this._http.post<TokenDto>(`${environment.baseUrl}/api/Authentication/login`, dto)
    }

    postForgotPassword(dto: ForgotPasswordDto) {
        return this._http.post(`${environment.baseUrl}/api/Authentication/forgot-password`, dto)
    }
}
