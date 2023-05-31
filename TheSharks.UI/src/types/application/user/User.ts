export interface User {
    id: string;
    userName: string;
    firstName: string;
    lastName: string;
    bio?: string;
    email: string;
    phoneNumber: string;
    profilePicture?: Blob;
}