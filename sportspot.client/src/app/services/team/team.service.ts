import { Injectable, WritableSignal, signal } from '@angular/core';
import { Team } from '../../models/team';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Game } from '../../models/game';
import { Player } from '../../models/player';
import { Position } from '../../models/position';
import { Rotation } from '../../models/rotation';
import { GameService } from '../game/game.service';
import { PlayerService } from '../player/player.service';
import { PositionService } from '../position/position.service';
import { RotationService } from '../rotation/rotation.service';
import { Observable, Subject } from 'rxjs';

@Injectable({
    providedIn: 'root'
})
export class TeamService {
    private readonly endpoint = '/api/Team';
    public team: WritableSignal<Team | undefined> = signal(undefined);

    constructor(
        private httpClient: HttpClient,
        private router: Router,
        private _snackBar: MatSnackBar,
        private gameService: GameService,
        private playerService: PlayerService,
        private positionService: PositionService,
        private rotationService: RotationService
    ) { }

    public getTeam(): void {
        this.httpClient.get<Team>(this.endpoint)
            .subscribe({
                next: (team) => {
                    this.team.set(team);
                },
                error: (error: any) => {
                    console.error(error);
                }
            });
    }

    public createTeam(team: Team, username: string, password: string): Observable<boolean> {
        let body = {
            team: team,
            username: username,
            password: password
        };

        const completedSubject = new Subject<boolean>();
        this.httpClient.post(this.endpoint, body)
            .subscribe({
                next: () => {
                    this.router.navigate(['']);
                    completedSubject.next(true);
                },
                error: (error: any) => {
                    console.error(error);
                    completedSubject.next(false);
                }
            });

        return completedSubject.asObservable();
    }

    public updateTeam(team: Team): void {
        this.httpClient.put<Team>(this.endpoint, team)
            .subscribe({
                next: () => {
                    this.team.set(team);
                },
                error: (error: any) => {
                    console.error(error);
                }
            });
    }

    public deleteTeam(): void {
        this.httpClient.delete<boolean>(this.endpoint)
            .subscribe({
                next: () => {
                    this._snackBar.open('Team deleted successfully!', 'Close');
                    this.router.navigate(['home']);
                },
                error: (error: any) => {
                    console.error(error);
                }
            });
    }

    public getGames(): void {
        this.httpClient.get<Game[]>(`${this.endpoint}/Game`)
            .subscribe({
                next: (games) => {
                    this.gameService.games.set(games);
                },
                error: (error: any) => {
                    console.error(error);
                }
            });
    }

    public getPlayers(): void {
        this.httpClient.get<Player[]>(`${this.endpoint}/Player`)
            .subscribe({
                next: (players) => {
                    this.playerService.players.set(players);
                },
                error: (error: any) => {
                    console.error(error);
                }
            });
    }

    public getPositions(): void {
        this.httpClient.get<Position[]>(`${this.endpoint}/Position`)
            .subscribe({
                next: (position) => {
                    this.positionService.positions.set(position);
                },
                error: (error: any) => {
                    console.error(error);
                }
            });
    }

    public getRotations(): void {
        this.httpClient.get<Rotation[]>(`${this.endpoint}/Rotation`)
            .subscribe({
                next: (rotations) => {
                    this.rotationService.rotations.set(rotations);
                },
                error: (error: any) => {
                    console.error(error);
                }
            });
    }
}
