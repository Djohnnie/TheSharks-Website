export interface UpdateActivityDto {
    id: string;
    title: string;
    type: string;
    location: string;
    locationLink: string;
    info: string;
    memberInfo: string;
    date: Date;
    departure?: Date;
    briefingTime?: Date;
    tide?: string;
    atWater?: Date;
    startTime?: Date;
    endTime?: Date;
}