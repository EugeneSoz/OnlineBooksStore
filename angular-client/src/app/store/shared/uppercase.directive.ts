import { Directive, HostListener, ElementRef } from '@angular/core';
import { NgControl } from '@angular/forms';

@Directive({
    selector: '[ngUpperCase]'
})
export class UpperCaseDirective {
    constructor(private el: ElementRef, private control: NgControl) {
    }

    @HostListener('input', ['$event']) onEvent($event) {
        const currentValue: string = this.control.value;
        this.control.control.setValue(currentValue.toUpperCase());
    }

}