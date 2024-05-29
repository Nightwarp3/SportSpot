import { Component } from '@angular/core';
import { PlayerService } from '../../services/player/player.service';
import { MatDialog } from '@angular/material/dialog';
import { PlayerModalComponent } from '../player-modal/player-modal.component';

@Component({
  selector: 'sportspot-player-list',
  templateUrl: './player-list.component.html',
  styleUrl: './player-list.component.scss'
})
export class PlayerListComponent {

    constructor(
        public playerService: PlayerService,
        private dialog: MatDialog
    ) { }

    public addPlayer(): void {
        this.dialog.open(PlayerModalComponent);
    }
}
