import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { EditProfileDto } from 'src/types/dto/user/EditProfileDto';
import { RegisterDto } from 'src/types/dto/user/RegisterDto';
import { ResetPasswordDto } from 'src/types/dto/user/ResetPasswordDto';
import { User } from 'src/types/application/user/User';

@Injectable({
    providedIn: 'root'
})
export class UserRestService {

    constructor(private _http: HttpClient) {

    }

    getUser(id: string) {
        return this._http.get<User>(`${environment.baseUrl}/api/Member/${id}`)
    }

    putEditProfile(dto: EditProfileDto) {
        const fd = new FormData()
        fd.append("id", dto.id)
        fd.append("phoneNumber", dto.phoneNumber)
        fd.append("bio", dto.bio);
        fd.append("ProfilePicture", dto.profilePicture);

        return this._http.put(`${environment.baseUrl}/api/Member/${dto.id}`, fd)
    }

    postRegisterUser(dto: RegisterDto) {
        return this._http.post(`${environment.baseUrl}/api/Authentication/register`, dto)
    }

    postResetPassword(dto: ResetPasswordDto) {
        return this._http.put(`${environment.baseUrl}/api/Member/${dto.id}/reset-password`, dto)
    }
}
