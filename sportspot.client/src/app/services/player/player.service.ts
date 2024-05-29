import { Injectable, WritableSignal, signal } from '@angular/core';
import { Player } from '../../models/player';
import { HttpClient } from '@angular/common/http';

@Injectable({
    providedIn: 'root'
})
export class PlayerService {
    public players: WritableSignal<Player[] | undefined> = signal(undefined);
    public selectedPlayer: WritableSignal<Player | undefined> = signal(undefined);
    public playerLoading: WritableSignal<boolean> = signal(false);

    private readonly playerUrl: string = '/api/Player';

    constructor(private http: HttpClient) { }

    public getPlayer(playerGuid: string): void {
        this.playerLoading.set(true);
        this.http.get<Player>(`${this.playerUrl}?playerId=${playerGuid}`).subscribe({
            next: (result) => {
                this.playerLoading.set(false);
                this.selectedPlayer.set(result);
            },
            error: (error) => {
                this.playerLoading.set(false);
                console.error(error);
            }
        });
    }

    public upsertPlayer(player: Player): void {
        this.playerLoading.set(true);
        this.http.post<Player>(this.playerUrl, player).subscribe({
            next: (result) => {
                let currentPlayers = this.players();
                if (currentPlayers) {
                    if (player.id !== result.id) {
                        currentPlayers.push(result);
                    } else {
                        let index = currentPlayers.findIndex((p) => p.id === result.id);
                        currentPlayers.splice(index, 1, result)
                    }
                    this.players.set(currentPlayers);
                }
                this.playerLoading.set(false);
            },
            error: (error) => {
                this.playerLoading.set(false);
                console.error(error);
            }
        });
    }

    public deletePlayer(player: Player): void {
        this.playerLoading.set(true);
        this.http.delete<boolean>(this.playerUrl, { body: player }).subscribe({
            next: (result) => {
                let currentPlayers = this.players();
                if (result) {
                    if (currentPlayers) {
                        let index = currentPlayers.findIndex((p) => p.id === player.id);
                        currentPlayers.splice(index, 1);
                        this.players.set(currentPlayers);
                    }
                }
                this.playerLoading.set(false);
            },
            error: (error) => {
                this.playerLoading.set(false);
                console.error(error);
            }
        });
    }
}
