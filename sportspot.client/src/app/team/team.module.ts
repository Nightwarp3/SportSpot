import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { LayoutModule } from '@angular/cdk/layout';
import { HttpClientModule } from '@angular/common/http';
import { ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatDialogModule } from '@angular/material/dialog';
import { MatDividerModule } from '@angular/material/divider';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatListModule } from '@angular/material/list';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { MatTableModule } from '@angular/material/table';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatTooltipModule } from '@angular/material/tooltip';
import { BrowserModule } from '@angular/platform-browser';
import { RouterOutlet, RouterLink, RouterLinkActive } from '@angular/router';

import { TeamRoutingModule } from './team-routing.module';
import { GameComponent } from './components/game/game.component';
import { GameDetailsComponent } from './components/game-details/game-details.component';
import { PlayerListComponent } from './components/player-list/player-list.component';
import { PlayerModalComponent } from './components/player-modal/player-modal.component';
import { AppRoutingModule } from '../app-routing.module';
import { TeamDashboardComponent } from './components/team-dashboard/team-dashboard.component';


@NgModule({
  declarations: [
    GameComponent,
    GameDetailsComponent,
    PlayerListComponent,
    PlayerModalComponent,
    TeamDashboardComponent
  ],
  imports: [
    CommonModule,
    TeamRoutingModule,
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    RouterOutlet,
    RouterLink,
    RouterLinkActive,
    ReactiveFormsModule,
    MatToolbarModule,
    MatButtonModule,
    MatCardModule,
    MatFormFieldModule,
    MatInputModule,
    MatTableModule,
    MatIconModule,
    MatListModule,
    MatDividerModule,
    MatTooltipModule,
    MatSnackBarModule,
    MatExpansionModule,
    MatProgressSpinnerModule,
    MatDialogModule,
    MatSidenavModule,
    LayoutModule
  ]
})
export class TeamModule { }
