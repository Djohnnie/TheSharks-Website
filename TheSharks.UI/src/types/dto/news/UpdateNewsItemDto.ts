export interface UpdateNewsItemDto {
    id: string;
    title: string;
    content: string;
    originalPublishDate: Date;
    membersOnly: boolean;
}