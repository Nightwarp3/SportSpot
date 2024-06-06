import { Component, OnInit } from '@angular/core';
import { TeamService } from './services/team/team.service';

@Component({
  selector: 'sportpot-app',
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})

export class AppComponent implements OnInit {
  title = 'Sport Spot Client';

  constructor(public teamService: TeamService) { }

  ngOnInit() {
  }
}
