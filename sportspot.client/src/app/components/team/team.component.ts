import { Component, OnInit, effect } from '@angular/core';
import { TeamService } from '../../services/team/team.service';
import { LayoutService } from '../../services/layout/layout.service';

@Component({
  selector: 'sportspot-team',
  templateUrl: './team.component.html',
  styleUrl: './team.component.scss'
})
export class TeamComponent implements OnInit {

    constructor(
        public teamService: TeamService,
        private layoutService: LayoutService
    ) {
        effect(() => {
            if (this.teamService.team()) {
                this.layoutService.hideSpinner();
                this.teamService.getPlayers();
                this.teamService.getGames();
                this.teamService.getPositions();
                this.teamService.getRotations();
            }
        });
    }

    public ngOnInit(): void {
        this.layoutService.showSpinner('Loading Team');
        this.teamService.getTeam();
    }
}
