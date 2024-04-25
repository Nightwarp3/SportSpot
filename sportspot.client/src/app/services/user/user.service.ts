import { HttpClient } from '@angular/common/http';
import { Injectable, WritableSignal, signal } from '@angular/core';

@Injectable({
    providedIn: 'root'
})
export class UserService {
    private readonly endpoint = '/api/User';

    public userValid: WritableSignal<boolean | undefined> = signal(undefined);
    public validatingUser: WritableSignal<boolean> = signal(false);

    constructor(private httpClient: HttpClient) { }

    public validateUsername(username: string): void {
        this.validatingUser.set(true);
        this.httpClient.get<boolean>(`/api/User/valid?username=${username}`)
            .subscribe({
                next: (valid: boolean) => {
                    this.validatingUser.set(false);
                    this.userValid.set(valid);
                },
                error: (err) => {
                    console.error(err);
                    this.validatingUser.set(false);
                    this.userValid.set(false);
                }
            });
    }
}
