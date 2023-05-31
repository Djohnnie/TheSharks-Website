import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { DocumentUri } from 'src/types/application/document/DocumentUri';
import { AddDocumentDto } from 'src/types/dto/document/AddDocumentDto';
import { DocumentListDto } from 'src/types/dto/document/DocumentListDto';

@Injectable({
    providedIn: 'root'
})
export class DocumentRestService {

    constructor(private _http: HttpClient) { }

    getDocumentsByPagination(page: number, size: number) {
        return this._http.get<DocumentListDto>(`${environment.baseUrl}/api/Document?Page=${page}&RecordsPerPage=${size}`)
    }

    getDocument(id: string) {
        return this._http.get<DocumentUri>(`${environment.baseUrl}/api/Document/${id}`)
    }

    deleteDocument(id: string, fileName: string) {
        return this._http.delete(`${environment.baseUrl}/api/Document/${id}?FileName=${fileName}&Id=${id}`, { body: { id: id } })
    }

    postDocument(dto: AddDocumentDto) {
        const fd = new FormData()
        fd.append("name", dto.name)
        fd.append("isImportant", dto.isImportant.toString())
        fd.append("file", dto.file)

        return this._http.post(`${environment.baseUrl}/api/Document`, fd)
    }
}
