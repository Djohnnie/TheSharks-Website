import { Injectable } from '@angular/core';
import { DocumentList } from 'src/types/application/document/DocumentList';
import { DocumentUri } from 'src/types/application/document/DocumentUri';
import { response } from '../response';
import { DocumentRestService } from './document-rest.service';

@Injectable({
    providedIn: 'root'
})
export class DocumentService {

    constructor(private _rest: DocumentRestService) { }

    async getDocuments(page: number, size: number) {
        return await response<DocumentList>(this._rest.getDocumentsByPagination(page, size))
    }

    async getDocumentUri(id: string) {
        return await response<DocumentUri>(this._rest.getDocument(id))
    }

    async addDocument(name: string, isImportant: boolean, file: File) {
        return await response(this._rest.postDocument({
            name: name,
            isImportant: isImportant,
            file: file
        }))
    }

    async deleteDocument(id: string, fileName: string) {
        return await response(this._rest.deleteDocument(id, fileName));
    }
}
