import { Injectable } from '@angular/core';
import {
    HttpClient,
    HttpErrorResponse,
    HttpHeaders,
} from '@angular/common/http';
import { catchError } from 'rxjs/operators';
import { BehaviorSubject, Observable, tap, throwError } from 'rxjs';
import { NewsDTO } from '../_Models/NewsDTO';
import { NewsAddDTO } from '../_Models/NewsAddDTO';
import { Router } from '@angular/router';

@Injectable({
    providedIn: 'root',
})
export class NewsService {
    api:string = 'https://localhost:7197/api/';

    constructor(
        private http: HttpClient,
        private router: Router,
    ) {}


    AddNews(News: NewsAddDTO, picture: File): Observable<any> {
        const formData = new FormData();
        formData.append('title', News.title);
        formData.append('newsContent', News.newsContent);
        formData.append('publicationDate', News.publicationDate);
        formData.append('image', News.image);
        formData.append('creationDate', News.creationDate);
        formData.append('authorId', News.authorId);
        formData.append('picture', picture);
      
        return this.http.post(
          `${this.api}News`,
          formData,
          { observe: 'response' }
        );
      }
    getNewsById(id: string) {
        return this.http.get<any>(
        `${this.api}News/${id}`
        );
    }
    deleteNews(id: string) {
        return this.http.delete(
            `${this.api}News/${id}`,
            { observe: 'response' }
        );
    }
    getNews() {
        return this.http.get<any>(`${this.api}News`);
    }
    editNews(id: string, news: NewsDTO,picture:File) {
        const formData = new FormData();
        formData.append('newsId', id);
        formData.append('title', news.title);
        formData.append('newsContent', news.newsContent);
        formData.append('image', news.image);
        formData.append('publicationDate', news.publicationDate);
        formData.append('creationDate', news.creationDate);
        formData.append('authorId', news.authorId);
        formData.append('picture', picture);
        return this.http.put(
        `${this.api}News/${id}`,
        formData,
        { observe: 'response' }
        );
    }

}
