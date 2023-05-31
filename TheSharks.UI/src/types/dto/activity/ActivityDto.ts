import { Participant } from "src/types/application/activity/Participant";

export interface ActivityDto {
    departure?: string;
    briefingTime?: string;
    tide?: string;
    atWater?: string;
    startTime?: string;
    endTime?: string;
    id: string;
    activityType: string;
    responsibleId: string;
    responsibleFirstName: string;
    responsibleLastName: string;
    title: string;
    name: string;
    date: string;
    location: string;
    locationLink: string;
    info: string;
    memberInfo: string;
    necessarySubscription: boolean;
    enrollments: Participant[]
}