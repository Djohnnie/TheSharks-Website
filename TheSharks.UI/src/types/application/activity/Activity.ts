import { Participant } from "./Participant";

export interface Activity {
    departure?: Date;
    briefingTime?: Date;
    tide?: string;
    atWater?: Date;
    startTime?: Date;
    endTime?: Date;
    id: string;
    activityType: string;
    responsibleId: string;
    responsibleFirstName: string;
    responsibleLastName: string;
    title: string;
    name: string;
    date: Date;
    location: string;
    locationLink: string;
    info: string;
    memberInfo: string;
    necessarySubscription: boolean;
    enrollments: Participant[]
}