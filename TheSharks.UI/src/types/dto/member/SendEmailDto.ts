export interface SendEmailDto {
    id: string;
    senderId: string;
    subject: string;
    message: string;
    checkedRecipients: string[]
    checkedRoles: string[]
}