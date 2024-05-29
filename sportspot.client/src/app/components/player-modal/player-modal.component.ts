import { Component, effect } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { PlayerService } from '../../services/player/player.service';
import { TeamService } from '../../services/team/team.service';
import { MatDialogRef } from '@angular/material/dialog';
import { LayoutService } from '../../services/layout/layout.service';

@Component({
  selector: 'sportspot-player-modal',
  templateUrl: './player-modal.component.html',
  styleUrl: './player-modal.component.scss'
})
export class PlayerModalComponent {

    public get name(): FormControl {
        return this.newPlayerFormGroup.get('name') as FormControl;
    }

    public newPlayerFormGroup: FormGroup = this.fb.group({
        id: ['00000000-0000-0000-0000-000000000000'],
        teamId: [this.teamService.team()?.id],
        name: ['', [Validators.required, Validators.minLength(3)]]
    });

    private savingPlayer: boolean = false;

    constructor(
        public playerService: PlayerService,
        public dialogRef: MatDialogRef<PlayerModalComponent>,
        private fb: FormBuilder,
        private teamService: TeamService,
        private layoutService: LayoutService
    ) {
        effect(() => {
            if (!this.playerService.playerLoading()) {
                this.layoutService.hideSpinner();

                if (this.savingPlayer) {
                    this.dialogRef.close();
                    this.savingPlayer = false;
                }
            }
        });
    }

    public createPlayer(): void {
        this.layoutService.showSpinner('Creating Player');
        this.savingPlayer = true;
        this.playerService.upsertPlayer(this.newPlayerFormGroup.value);
    }
}
