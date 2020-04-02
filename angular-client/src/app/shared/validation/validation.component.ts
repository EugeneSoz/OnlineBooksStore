import { Component, Input } from "@angular/core";

@Component({
    selector: 'bs-validation',
    templateUrl: './validation.component.html',
})
export class ValidationComponent {
    @Input() messages: Array<string> = null;
}
