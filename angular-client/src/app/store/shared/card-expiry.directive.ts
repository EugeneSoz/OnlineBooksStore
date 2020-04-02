import { Directive, HostListener, Self } from '@angular/core';
import { NgControl } from '@angular/forms';

@Directive({
    selector: '[ngCardExpiry]'
})
export class CardExpiryDirective {
    constructor(@Self() private control: NgControl) {
    }

    @HostListener('input', ['$event']) onEvent($event) {
        const currentValue: string = this.control.value;
        if (currentValue.length == 2) {
            this.control.control.setValue(`${currentValue}/`);
        }
    }
}