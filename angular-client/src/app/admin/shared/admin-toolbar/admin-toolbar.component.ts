import { Component, Input } from "@angular/core";
import { AuthenticationService } from '../../../authentication/shared/authentication.service';

@Component({
    selector: 'bs-admin-toolbar',
    templateUrl: './admin-toolbar.component.html',
})
export class AdminToolbarComponent {
    constructor(
        private _authService: AuthenticationService) { }

    @Input() isButtonsVisible: boolean = false;
    @Input() link: string = "";

    isCollapsed: boolean = true;
    get userName(): string {
        return this._authService.name;
    }

    logout(): void {
        this._authService.logout();
    }
}
