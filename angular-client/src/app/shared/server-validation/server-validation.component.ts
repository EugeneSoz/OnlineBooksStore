import { Component, Input } from "@angular/core";

@Component({
    selector: 'bs-server-validation',
    templateUrl: './server-validation.component.html',
})
export class ServerValidationComponent {
    @Input() errors: Array<string> = null;
}
