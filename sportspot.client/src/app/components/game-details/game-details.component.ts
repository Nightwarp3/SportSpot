import { Component, effect } from '@angular/core';
import { GameService } from '../../services/game/game.service';
import { Game } from '../../models/game';

@Component({
    selector: 'sportspot-game-details',
    templateUrl: './game-details.component.html',
    styleUrl: './game-details.component.scss'
})
export class GameDetailsComponent {
    public rotations: any[] = [];

    constructor(private gameService: GameService) {
        effect(() => {
            let newGame = this.gameService.game();
            if (newGame) {
                this.processGame(newGame)
            }
        });
    }

    public getGame(): void {
        this.rotations = [];
        this.gameService.getGame();
    }

    private processGame(game: Game): void {
        let playerCounters: any = {};
        if (this)
            for (let sub of game.substitutions) {
                if (!playerCounters.hasOwnProperty(sub.player.name)) {
                    playerCounters[sub.player.name] = 0;
                }

                if (sub.position.name === 'Sub') {
                    playerCounters[sub.player.name]++;
                }

                sub.playerCount = playerCounters[sub.player.name];

                let existingRotationIndex = this.rotations.findIndex(x => x.name === sub.rotation.name);
                let rotation;
                if (existingRotationIndex === -1) {
                    rotation = { name: sub.rotation.name, substitutions: [] };
                    this.rotations.push(rotation);
                } else {
                    rotation = this.rotations[existingRotationIndex];
                }

                rotation.substitutions.push(sub);
            }
    }
}
