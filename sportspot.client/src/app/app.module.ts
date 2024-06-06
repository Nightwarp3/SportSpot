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
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatDialogModule } from '@angular/material/dialog';
import { MatSidenavModule } from '@angular/material/sidenav';
import { LayoutModule } from '@angular/cdk/layout';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { HomeComponent } from './components/home/home.component';
import { MatTooltipModule } from '@angular/material/tooltip';
import { SpinnerComponent } from './components/spinner/spinner.component';
import { AuthInterceptor } from './interceptors/auth/auth.interceptor';
import { ToolbarComponent } from './components/toolbar/toolbar.component';
import { RouterLink, RouterLinkActive, RouterOutlet } from '@angular/router';
import { LoginComponent } from './components/login/login.component';
import { TeamModule } from './team/team.module';

@NgModule({
    declarations: [
        AppComponent,
        HomeComponent,
        SpinnerComponent,
        ToolbarComponent,
        LoginComponent
    ],
    imports: [
        BrowserModule,
        HttpClientModule,
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
        LayoutModule,
        TeamModule,
        
        AppRoutingModule
    ],
    providers: [
        provideAnimationsAsync(),
        { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true },
        // {provide: MAT_SNACK_BAR_DEFAULT_OPTIONS, useValue: { duration: 3000, verticalPosition: 'top', horizontalPosition: 'center' }}
    ],
    bootstrap: [AppComponent]
})
export class AppModule { }
