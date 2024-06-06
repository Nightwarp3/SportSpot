import { Component } from '@angular/core';
import { TeamService } from '../../services/team/team.service';
import { AuthService } from '../../services/auth/auth.service';
import { BreakpointObserver, Breakpoints } from '@angular/cdk/layout';

@Component({
  selector: 'sportspot-toolbar',
  templateUrl: './toolbar.component.html',
  styleUrl: './toolbar.component.scss'
})
export class ToolbarComponent {
    public showSidenav: boolean = true;

    constructor(
        public teamService: TeamService,
        public authService: AuthService,
        private breakpointObserver: BreakpointObserver
    ) {
        breakpointObserver.observe([
            Breakpoints.Medium,
            Breakpoints.Large,
            Breakpoints.XLarge,
        ]).subscribe(result => {
            this.showSidenav = result.matches;
        });
    }

}
