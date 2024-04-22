import { HttpClient } from '@angular/common/http';
import { Injectable, WritableSignal, signal } from '@angular/core';
import { Game } from '../../models/game';

@Injectable({
    providedIn: 'root'
})

export class GameService {
    public game: WritableSignal<Game | undefined> = signal(undefined);

    constructor(private http: HttpClient) { }

    public getGame(): void {
        this.http.get<Game>('/api/Game').subscribe(
            (result) => {
              this.game.set(result);
            },
            (error) => {
              console.error(error);
            }
          );
    }
}
