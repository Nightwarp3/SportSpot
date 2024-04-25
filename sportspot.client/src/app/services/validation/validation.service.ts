import { Injectable } from '@angular/core';
import { AbstractControl, ValidationErrors } from '@angular/forms';

@Injectable({
    providedIn: 'root'
})
export class ValidationService {

    constructor() { }

    public passwordsMatchValidator(confirmPasswordControl: AbstractControl): ValidationErrors | null {
        const password = confirmPasswordControl.parent?.get('password')?.value;
        const confirmPassword = confirmPasswordControl.value;

        if (password !== confirmPassword) {
            return { passwordsMatch: true };
        }

        return null;
    }

    public passwordRequirementsValidator(passwordControl: AbstractControl): ValidationErrors | null {
        const password = passwordControl.value;
        let errors: any = {};

        if (!/[A-Z]/.test(password)) {
            errors.uppercase = true;
        }

        if (!/[a-z]/.test(password)) {
            errors.lowercase = true;
        }

        if (!/[0-9]/.test(password)) {
            errors.number = true;
        }

        return errors.hasOwnProperty('uppercase') || errors.hasOwnProperty('lowercase') || errors.hasOwnProperty('number') ? errors : null;
    }
}
