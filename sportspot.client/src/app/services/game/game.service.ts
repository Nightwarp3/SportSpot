import { HttpClient } from '@angular/common/http';
import { Injectable, WritableSignal, signal } from '@angular/core';
import { Game } from '../../models/game';
import { Observable, catchError, throwError } from 'rxjs';
import { Substitution } from '../../models/substitution';
import { SubstitutionService } from '../substitution/substitution.service';

@Injectable({
    providedIn: 'root'
})

export class GameService {
    private readonly gameUrl: string = '/api/Game';
    public games: WritableSignal<Game[] | undefined> = signal(undefined);
    public currentGame: WritableSignal<Game | undefined> = signal(undefined);

    constructor(private http: HttpClient, private substitutionService: SubstitutionService) { }

    public getGame(gameGuid: string): void {
        this.http.get<Game>(`${this.gameUrl}?gameGuid=${gameGuid}`).subscribe({
            next: (result) => {
                this.currentGame.set(result);
            },
            error: (error) => {
                console.error(error);
            }
        });
    }

    public upsertGame(game: Game): void {
        this.http.post<Game>(this.gameUrl, game).subscribe({
            next: (result) => {
                this.currentGame.set(result);
            },
            error: (error) => {
                console.error(error);
            }
        });
    }

    public deleteGame(game: Game): Observable<boolean> {
        return this.http.delete<boolean>(this.gameUrl, { body: game })
            .pipe(
                catchError((err: any) => throwError(() => new Error(err)))
            );
    }

    public getSubstitutions(gameGuid: string): void {
        this.http.get<Substitution[]>(`${this.gameUrl}/${gameGuid}/Substitution`).subscribe({
            next: (result) => {
                this.substitutionService.substitutions.set(result);
            },
            error: (error) => {
                console.error(error);
            }
        });
    }
}
