import { Injectable } from '@angular/core';
import {
    HttpClient,
    HttpErrorResponse,
    HttpHeaders,
} from '@angular/common/http';
import { catchError } from 'rxjs/operators';
import { BehaviorSubject, Observable, tap, throwError } from 'rxjs';
import { AuthorDTO } from '../_Models/AuthorDTO';
import { AuthorAddDTO } from '../_Models/AuthorAddDTO';
import { Router } from '@angular/router';

@Injectable({
    providedIn: 'root',
})
export class AuthorService {
    private addAuthorSubject = new BehaviorSubject<boolean>(false);
    addAuthorObservable = this.addAuthorSubject.asObservable();

    api:string = 'https://localhost:7197/api/';

    constructor(
        private http: HttpClient,
        private router: Router,
    ) {}


    AddAuthor(Author: AuthorAddDTO) {
        return this.http.post(
        `${this.api}Authors`,
        Author,
        { observe: 'response' }
        );
    }
    addAuthorCompleted() {
        this.addAuthorSubject.next(true);
    }
    getAuthorById(id: string) {
        return this.http.get<any>(
        `${this.api}Authors/${id}`
        );
    }
    deleteAuthor(id: string) {
        return this.http.delete(
            `${this.api}Authors/${id}`,
            { observe: 'response' }
        );
    }
    getAuthors() {
        return this.http.get<any>(`${this.api}Authors`);
    }
    editAuthor(id: string, editedAuthor: AuthorDTO) {
        return this.http.put(
        `${this.api}Authors/${id}`,
        editedAuthor,
        { observe: 'response' }
        );
    }
}
