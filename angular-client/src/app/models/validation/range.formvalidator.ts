import { AbstractControl, ValidationErrors } from '@angular/forms';

export class RangeValidator {
    static range(min: number, max: number) {
        return (control: AbstractControl): ValidationErrors | null => {
            let valueLength: number = control == null || control.value == null
                ? 0
                : (control.value as string).length;

            return valueLength < min || valueLength > max
                ? { 'range': { value: control.value } }
                : null;
        };
    }
}
