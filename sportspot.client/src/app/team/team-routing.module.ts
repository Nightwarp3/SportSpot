import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { authGuard } from '../guards/auth.guard';
import { GameComponent } from './components/game/game.component';
import { PlayerListComponent } from './components/player-list/player-list.component';
import { TeamDashboardComponent } from './components/team-dashboard/team-dashboard.component';

const routes: Routes = [
    { 
        path: 'team-dashboard',
        component: TeamDashboardComponent,
        canActivate: [authGuard],
        children: [
            { path: '', component: TeamDashboardComponent },
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
