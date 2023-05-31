import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { EnrollmentsDto } from 'src/types/dto/enrollment/EnrollmentsDto';

@Injectable({
    providedIn: 'root'
})
export class EnrollmentRestService {

    constructor(private _http: HttpClient) {

    }

    postEnrollment(dto: EnrollmentsDto) {
        return this._http.post(`${environment.baseUrl}/api/Enrollment`, dto)
    }

    deleteEnrollment(memberId: string, activityId: string) {
        return this._http.delete(`${environment.baseUrl}/api/Enrollment`, {
            body: {
                memberId: memberId,
                activityId: activityId
            }
        })
    }
}
