import { Injectable } from '@angular/core';
import { BehaviorSubject, firstValueFrom } from 'rxjs';
import { User } from 'src/types/application/user/User';
import { AuthService } from '../auth/auth.service';
import { UserRestService } from './user-rest.service';
import { response } from '../response';

@Injectable({
    providedIn: 'root'
})
export class UserService {
    private _currentUser$ = new BehaviorSubject<User | undefined>(undefined);
    currentUser$ = this._currentUser$.asObservable()

    constructor(private _rest: UserRestService, private _as: AuthService) {
    }

    async getUser() {
        const token = this._as.getDecodedToken()

        if (token === null) return { response: undefined }

        if (this._currentUser$.value === undefined) return this.forceGetUser()

        if (token.sub === this._currentUser$.value.id) return { response: await firstValueFrom(this.currentUser$) }

        return this.forceGetUser()
    }

    async forceGetUser() {
        return response<User>(this._rest.getUser(this._as.getDecodedToken().sub), (r) => this._currentUser$.next(r))
    }

    async editUser(id: string, phoneNumber: string, bio: string, picture: File) {
        const o$ = this._rest.putEditProfile({
            id: id,
            phoneNumber: phoneNumber,
            bio: bio,
            profilePicture: picture
        })

        return response(o$)
    }

    async registerUser(firstName: string, lastName: string, userName: string, email: string) {
        const o$ = this._rest.postRegisterUser({
            firstName: firstName,
            lastName: lastName,
            userName: userName,
            email: email
        })

        return response(o$)
    }

    async resetPassword(id: string, token: string, newPassword: string) {
        const o$ = this._rest.postResetPassword({
            id: id,
            token: token,
            newPassword: newPassword
        })

        return response(o$)
    }

    async removeUser(id: string) {

    }
}