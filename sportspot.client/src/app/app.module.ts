import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { ReactiveFormsModule } from '@angular/forms';

import { MatToolbarModule } from '@angular/material/toolbar';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatTableModule } from '@angular/material/table';
import { MatIconModule } from '@angular/material/icon';
import { MatListModule } from '@angular/material/list';
import { MatDividerModule } from '@angular/material/divider';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MAT_SNACK_BAR_DEFAULT_OPTIONS, MatSnackBarModule } from '@angular/material/snack-bar';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatDialogModule } from '@angular/material/dialog';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { TeamComponent } from './components/team/team.component';
import { GameComponent } from './components/game/game.component';
import { GameDetailsComponent } from './components/game-details/game-details.component';
import { HomeComponent } from './components/home/home.component';
import { MatTooltipModule } from '@angular/material/tooltip';
import { SpinnerComponent } from './components/spinner/spinner.component';
import { AuthInterceptor } from './interceptors/auth/auth.interceptor';
import { PlayerListComponent } from './components/player-list/player-list.component';
import { PlayerModalComponent } from './components/player-modal/player-modal.component';

@NgModule({
    declarations: [
        AppComponent,
        TeamComponent,
        GameComponent,
        GameDetailsComponent,
        HomeComponent,
        SpinnerComponent,
        PlayerListComponent,
        PlayerModalComponent
    ],
    imports: [
        BrowserModule,
        HttpClientModule,
        AppRoutingModule,
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
        MatDialogModule
    ],
    providers: [
        provideAnimationsAsync(),
        { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true },
        // {provide: MAT_SNACK_BAR_DEFAULT_OPTIONS, useValue: { duration: 3000, verticalPosition: 'top', horizontalPosition: 'center' }}
    ],
    bootstrap: [AppComponent]
})
export class AppModule { }
