import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators, FormBuilder } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { debounceTime, filter } from 'rxjs';
import { AuthService } from '../../services/auth/auth.service';
import { TeamService } from '../../services/team/team.service';
import { UserService } from '../../services/user/user.service';
import { ValidationService } from '../../services/validation/validation.service';

@Component({
  selector: 'sportspot-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent {
    public get password(): FormControl {
        return this.newTeamFormGroup.get('password') as FormControl;
    }

    public get confirmPassword(): FormControl {
        return this.newTeamFormGroup.get('confirmPassword') as FormControl;
    }

    public get username(): FormControl {
        return this.newTeamFormGroup.get('username') as FormControl;
    }

    public existingTeamFormGroup: FormGroup = this.fb.group({
        password: ['', [Validators.required, Validators.minLength(8)]]
    });

    public showLogin: boolean = true;

    public loginFormGroup: FormGroup = this.fb.group({
        username: ['', [Validators.required, Validators.minLength(3)]],
        password: ['', [Validators.required, Validators.minLength(8)]]
    });

    public newTeamFormGroup: FormGroup = this.fb.group({
        team: this.fb.group({
            id: ['00000000-0000-0000-0000-000000000000'],
            name: ['', [Validators.required, Validators.minLength(3)]],
            description: ['', [Validators.required, Validators.minLength(3)]]
        }),
        username: ['', [Validators.required, Validators.minLength(3)]],
        password: ['', [Validators.required, Validators.minLength(8), this.validationService.passwordRequirementsValidator]],
        confirmPassword: ['', [Validators.required, Validators.minLength(8), this.validationService.passwordsMatchValidator]],
    });

    constructor(
        private fb: FormBuilder,
        private validationService: ValidationService,
        public userService: UserService,
        public teamService: TeamService,
        private authService: AuthService,
        private snackBar: MatSnackBar,
        private router: Router
    ) {
        this.username?.valueChanges
            .pipe(
                debounceTime(500),
                filter(x => this.username.valid)
            ).subscribe(username => this.userService.validateUsername(username));

        if (this.authService.JwtToken?.length > 0) {
            this.authService.authorized.set(true);
            this.router.navigate(['team-dashboard']);
        }
    }

    public login(loginGroup: FormGroup) {
        if (loginGroup.invalid) {
            return;
        }

        this.authService.authorizeUser(loginGroup.get('username')?.value || '', loginGroup.get('password')?.value)
            .subscribe((success: boolean) => {
                if (!success) {
                    this.snackBar.open('Invalid username or password', "Ok", { panelClass: ['snackbar-error']});
                }
            });
    }

    public createTeam() {
        if (this.newTeamFormGroup.invalid) {
            return;
        }

        this.teamService.createTeam(this.newTeamFormGroup.get('team')?.value, this.username.value, this.password.value)
            .subscribe((success: boolean) => {
                if (success) {
                    this.snackBar.open('Team created successfully', "Ok", { panelClass: ['snackbar-success']});
                } else {
                    this.snackBar.open('Failed to create team', "Ok", { panelClass: ['snackbar-error']});
                }
            });
    }
}
