import { HttpClient } from '@angular/common/http';
import { Injectable, WritableSignal, signal } from '@angular/core';
import { Router } from '@angular/router';
import { Observable, Subject } from 'rxjs';

@Injectable({
    providedIn: 'root'
})
export class AuthService {
    private readonly storageKey = 'sportspot-jwtBearerToken';

    public authorized: WritableSignal<boolean> = signal(false);

    public get JwtToken(): string {
        return localStorage.getItem(this.storageKey) as string;
    }

    public set JwtToken(token: string) {
        localStorage.setItem(this.storageKey, token);
    }

    constructor(private httpClient: HttpClient, private router: Router) {
        if(!!this.JwtToken) {
            this.authorized.set(true);
            this.router.navigate(['team-dashboard']);
        }
    }

    public authorizeUser(username: string, password: string): Observable<boolean> {
        let body = {
            username: username,
            password: password
        };

        const completedSubject = new Subject<boolean>();
        this.httpClient.post<string>('/api/Authenticate', body)
            .subscribe({
                next: (token: string) => {
                    this.JwtToken = token;
                    this.authorized.set(token.length > 0);
                    this.router.navigate(['team-dashboard'])
                    completedSubject.next(true);
                },
                error: (err) => {
                    console.log(err);
                    completedSubject.next(false);
                }
            });
        
        return completedSubject.asObservable();
    }

    public logout(): void {
        localStorage.removeItem(this.storageKey);
        this.authorized.set(false);
        this.router.navigate(['home']);
    }
}
