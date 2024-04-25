import { Component, OnInit } from '@angular/core';
import { TeamService } from './services/team/team.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})

export class AppComponent implements OnInit {
  title = 'sportspot.client';

  constructor(public teamService: TeamService) { }

  ngOnInit() {
  }
}
