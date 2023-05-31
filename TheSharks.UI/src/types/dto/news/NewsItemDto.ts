export interface NewsItemDto {
    id: string;
    authorId: string;
    authorFirstName: string;
    authorProfilePicture: Blob;
    authorLastName: string;
    title: string;
    content: string;
    publishDate: string;
    membersOnly: boolean;
}