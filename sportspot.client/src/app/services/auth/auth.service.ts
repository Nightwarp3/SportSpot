import { HttpClient } from '@angular/common/http';
import { Injectable, WritableSignal, signal } from '@angular/core';

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

    constructor(private httpClient: HttpClient) { }

    public authorizeUser(password: string): void {
        this.httpClient.post<string>('/api/Authentiate', password)
            .subscribe({
                next: (token: string) => {
                    this.JwtToken = token;
                    this.authorized.set(this.JwtToken.length> 0);
                },
                error: (err) => {
                    console.log(err);
                }
            })
    }
}
