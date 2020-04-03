import { Injectable } from "@angular/core";
import { Router, ActivatedRouteSnapshot, RouterStateSnapshot } from "@angular/router";

import { AuthenticationService } from "./authentication.service";
import { PageLink } from "../../../domain/model/url/page-link.model";

@Injectable({ providedIn: "root" })
export class AuthenticationGuard {
    constructor(
        private _router: Router,
        private _authService: AuthenticationService) { }

    canActivateChild(route: ActivatedRouteSnapshot,
        state: RouterStateSnapshot): boolean {
        if (this._authService.authenticated) {
            return true;
        } else {
            this._router.navigateByUrl(`/${PageLink.login}`);
            return false;
        }
    }
}
