import { EnrollmentGuestDto } from "./EnrollmentGuestDto";
import { EnrollmentMemberDto } from "./EnrollmentMemberDto";

export interface EnrollmentsDto {
    activityId: string;
    registratorId: string;
    memberEnrollments: EnrollmentMemberDto[];
    guestEnrollments: EnrollmentGuestDto[];
}