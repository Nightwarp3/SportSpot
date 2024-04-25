import { Injectable, WritableSignal, signal } from '@angular/core';
import { Team } from '../../models/team';
import { HttpClient } from '@angular/common/http';

@Injectable({
    providedIn: 'root'
})
export class TeamService {
    private readonly endpoint = '/api/Team';
    public team: WritableSignal<Team | undefined> = signal(undefined);

    constructor(private httpClient: HttpClient) { }

    public getExistingTeam(password: string): void {
        this.httpClient.get<Team>(this.endpoint);
    }

    public createNewTeam(team: Team, password: string): void {
        let body = {
            team: team,
            password: password
        };
        this.httpClient.post<Team>(this.endpoint, body)
            .subscribe(

            )
    }
}
