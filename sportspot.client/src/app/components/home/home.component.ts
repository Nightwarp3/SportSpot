import { Component } from '@angular/core';
import { AbstractControl, FormBuilder, FormControl, FormGroup, ValidationErrors, Validators } from '@angular/forms';
import { ValidationService } from '../../services/validation/validation.service';
import { UserService } from '../../services/user/user.service';
import { debounceTime, filter } from 'rxjs';

@Component({
    selector: 'sportspot-home',
    templateUrl: './home.component.html',
    styleUrl: './home.component.scss'
})
export class HomeComponent {
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
        public userService: UserService
    ) {
        this.username?.valueChanges
            .pipe(
                debounceTime(500),
                filter(x => this.username.valid)
            ).subscribe(username => this.userService.validateUsername(username));
    }

    public createNewTeam() {
        if (this.newTeamFormGroup.invalid) {
            return;
        }
    }
}
