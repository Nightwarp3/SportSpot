import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})

export class AppComponent implements OnInit {
  public game: Game | undefined;
  public rotations: any[] = [];

  constructor(private http: HttpClient) { }

  ngOnInit() {
    this.getGame();
  }

  getGame() {
    this.rotations = [];
    this.http.get<Game>('/api/Game').subscribe(
      (result) => {
        this.game = result;
        let playerCounters: any = {};
        for (let sub of this.game.substitutions) {
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
      },
      (error) => {
        console.error(error);
      }
    );
  }

  title = 'sportspot.client';
}

export interface Game {
  substitutions: any[];
}
