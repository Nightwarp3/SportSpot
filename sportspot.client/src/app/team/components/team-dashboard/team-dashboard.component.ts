import { Component, effect } from '@angular/core';
import { LayoutService } from '../../../services/layout/layout.service';
import { TeamService } from '../../../services/team/team.service';

@Component({
  selector: 'sportspot-team-dashboard',
  templateUrl: './team-dashboard.component.html',
  styleUrl: './team-dashboard.component.scss'
})
export class TeamDashboardComponent {
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
        this.layoutService.showSpinner('Loading Team');
        this.teamService.getTeam();
    }

    
}
