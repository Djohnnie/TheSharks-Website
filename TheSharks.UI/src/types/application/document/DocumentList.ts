import { DocumentListItemDto } from "src/types/dto/document/DocumentListItemDto";

export interface DocumentList {
    totalRecords: number;
    documents: DocumentListItemDto[]
}