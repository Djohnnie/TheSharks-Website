export interface AddActivityDto {
    departure?: Date;
    briefingTime?: Date;
    tide?: string;
    atWater?: Date;
    startTime?: Date;
    endTime?: Date;
    id: string;
    activityType: string;
    responsibleId: string;
    name: string;
    title: string;
    date: Date;
    location: string;
    locationLink: string;
    info: string;
    memberInfo: string;
    necessarySubscription: boolean;

}