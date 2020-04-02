import { AbstractControl, ValidationErrors } from '@angular/forms';

export class NotNullMinValidator {
    static notNullMin(min: number) {
        return (control: AbstractControl): ValidationErrors | null => {
            let controlValue: number = control == null || control.value == null
                ? 0
                : Number.parseInt(control.value);

            return controlValue < min
                ? { 'notNullMin': { value: control.value } }
                : null;
        };
    }
}
