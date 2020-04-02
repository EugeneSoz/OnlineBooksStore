import { Component } from "@angular/core";

import { AuthenticationService } from './shared/authentication.service';

@Component({
    templateUrl: './authentication.component.html',
})
export class AuthenticationComponent {
    constructor(
        public authService: AuthenticationService) { }

    showError: boolean = false;

    login(): void {
        this.showError = false;
         this.authService.login().subscribe(result => {
             this.showError = !result;
        });
    }
}
