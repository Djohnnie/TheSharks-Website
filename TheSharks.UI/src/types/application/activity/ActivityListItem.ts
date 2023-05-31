export interface ActivityListItem {
    id: string;
    name: string;
    date: Date;
    location: string;
    locationLink: string;
    activityType: string;
    responsibleFirstName: string;
    responsibleLastName: string;
    departure?: Date;
    briefingTime?: Date;
    tide?: string;
    atWater?: Date;
    startTime?: Date;
    endTime?: Date;
    memberCount?: number;
    userEnrolled?: boolean;
}