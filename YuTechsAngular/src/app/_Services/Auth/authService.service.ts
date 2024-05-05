import { Injectable } from '@angular/core';
import {
    HttpClient,
    HttpErrorResponse,
    HttpHeaders,
} from '@angular/common/http';
import { catchError } from 'rxjs/operators';
import { BehaviorSubject, Observable, tap, throwError } from 'rxjs';

import { TokenDTO } from 'src/app/_Models/TokenDTO';
import { Router } from '@angular/router';
export interface AuthResponseData {
    token: string;
    expiryDate: string;
    username: string;
    user_id: string;
}

@Injectable({
    providedIn: 'root',
})
export class AuthService {
    api:string = 'https://localhost:7197/api/';
    token : TokenDTO = new TokenDTO("","",new Date,"");
    user = new BehaviorSubject<TokenDTO>(this.token);
    private tokenExpirationTimer: any;

    constructor(
        private http: HttpClient,
        private router: Router,
    ) {}

    signup(user: {
        name: string;
        userName: string;
        password: string;
    }): Observable<any> {
        return this.http.post<AuthResponseData>(
            this.api+'Admins/register',
        user
        );
    }
    login(username: string, password: string): Observable<any> {
        return this.http
            .post<AuthResponseData>(
                this.api+'Admins/login',
                {
                    username, // Corrected here
                    password,
                }
            )
            .pipe(
                tap((resData) =>
                    this.handleAuthentication(
                        resData.token,
                        resData.username,
                        resData.expiryDate,
                        resData.user_id,
                    )
                )
            );
    }
    

    getAdminById(userid: string) {
        return this.http.get<any>(
        `${this.api}Admins/${userid}`
        );
    }
    deleteAdmin(userId: string) {
        return this.http.delete(
            `${this.api}Admins/${userId}`,
            { observe: 'response' }
        );
    }

    handleError(error: HttpErrorResponse) {
        let errorMessage = 'Unknown error occurred.';
        if (error.error instanceof ErrorEvent) {
        // Client-side error
        errorMessage = `Error: ${error.error.message}`;
        } else {
        // Server-side error
        errorMessage = `Backend returned code ${
            error.status
        }, body was: ${JSON.stringify(error.error)}`;
        }
        console.error(errorMessage);
        return throwError(errorMessage);
    }
    autoLogin() {
        const userData = JSON.parse(localStorage.getItem('userData') ?? '{}');
        if (!userData) {
            return;
        }
        console.log(userData);
        const loadedUser = new TokenDTO(
            userData._token,
            userData.userName,
            userData._tokenExpirationDate,
            userData.user_id,
        );
    
        if (loadedUser.token) {
            this.user.next(loadedUser);
            const newExpirationDate =
                new Date(userData._tokenExpirationDate)?.getTime() -
                new Date().getTime();
    
            this.autoLogout(newExpirationDate);
    
            console.log(loadedUser._tokenExpirationDate);
            console.log(newExpirationDate);
        }
    }
    

    logout() {
        this.user.next(new TokenDTO('', '', new Date(), '')); // Clearing the user BehaviorSubject
        this.router.navigate(['/login']);
        localStorage.removeItem('userData'); // Removing user data from localStorage
        if (this.tokenExpirationTimer) {
            clearTimeout(this.tokenExpirationTimer);
        }
        this.tokenExpirationTimer = null;
    }
    
    getusers() {
        return this.http.get<any>(`${this.api}Admins`);
    }
    autoLogout(expirationDuration: number) {
        this.tokenExpirationTimer = setTimeout(
        () => this.logout(),
        expirationDuration
        );
    }

    private handleAuthentication(
        token: string,
        userName: string,
        expiryDate: string,
        user_id: string,
    ) {
        const user: TokenDTO = new TokenDTO(
            token,
            userName,
            new Date(expiryDate),
            user_id,
        );
        this.user.next(user);
        // expiry time in seconds * 1000 (to convert to milliseconds)
        this.autoLogout(7200 * 1000);
        localStorage.setItem('userData', JSON.stringify(user));
    }
}
