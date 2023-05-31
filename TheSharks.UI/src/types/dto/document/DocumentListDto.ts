import { DocumentListItemDto } from "./DocumentListItemDto";

export interface DocumentListDto {
    totalRecords: number;
    documents: DocumentListItemDto[]
}