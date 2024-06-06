import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TeamComponent } from './components/team/team.component';
import { authGuard } from '../guards/auth.guard';
import { GameComponent } from './components/game/game.component';
import { PlayerListComponent } from './components/player-list/player-list.component';

const routes: Routes = [
    { 
        path: 'team',
        component: TeamComponent,
        canActivate: [authGuard],
        children: [
            { path: '', component: TeamComponent },
            { path: 'game', component: GameComponent },
            { path: 'player', component: PlayerListComponent },
            { path: 'rotation', component: PlayerListComponent },
            { path: 'position', component: PlayerListComponent },
        ]
    },];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class TeamRoutingModule { }
