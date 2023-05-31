export interface NewsItem {
    id: string;
    authorId: string;
    authorFirstName: string;
    authorLastName: string;
    title: string;
    content: string;
    publishDate: Date;
    membersOnly: boolean;
}