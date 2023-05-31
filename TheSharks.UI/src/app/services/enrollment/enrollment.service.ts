import { Injectable } from '@angular/core';
import { BaseResponse } from 'src/types/BaseResponse';
import { EnrollmentGuestDto } from 'src/types/dto/enrollment/EnrollmentGuestDto';
import { EnrollmentMemberDto } from 'src/types/dto/enrollment/EnrollmentMemberDto';
import { EnrollmentsDto } from 'src/types/dto/enrollment/EnrollmentsDto';
import { EnrollmentGuest } from 'src/types/application/enrollment/EnrollmentGuest';
import { EnrollmentRestService } from './enrollment-rest.service';
import { EnrollmentMember } from 'src/types/application/enrollment/EnrollmentMember';
import { response } from '../response';

@Injectable({
    providedIn: 'root'
})
export class EnrollmentService {

    constructor(private _rest: EnrollmentRestService) {

    }

    async addEnrollments(activityId: string, registratorId: string, memberEnrollments: EnrollmentMember[], guestEnrollments: EnrollmentGuest[]): Promise<BaseResponse<any>> {
        const mes: EnrollmentMemberDto[] = memberEnrollments.map<EnrollmentMemberDto>((e) => {
            return {
                ...e,
                registratorId: registratorId,
                activityId: activityId
            }
        })

        const ges: EnrollmentGuestDto[] = guestEnrollments.map<EnrollmentGuestDto>((e) => {
            return {
                ...e,
                registratorId: registratorId,
                activityId: activityId
            }
        })

        const dto: EnrollmentsDto = {
            activityId: activityId,
            registratorId: registratorId,
            memberEnrollments: mes,
            guestEnrollments: ges
        }

        const o$ = this._rest.postEnrollment(dto)
        return response(o$)
    }

    deleteEnrollment(memberId: string, activityId: string) {
        return response(this._rest.deleteEnrollment(memberId, activityId))
    }
}
