import { Directive, Self, HostListener } from '@angular/core';
import { NgControl } from '@angular/forms';

@Directive({
    selector: '[ngCardNumber]'
})
export class CardNumberDirective {
    constructor(@Self() private control: NgControl) { }

    @HostListener('input', ['$event']) onEvent($event) {
        const currentValue: string = this.control.value;
        if (currentValue.length == 4 || currentValue.length == 9 || currentValue.length == 14) {
            this.control.control.setValue(`${currentValue} `);
        }
    }
}