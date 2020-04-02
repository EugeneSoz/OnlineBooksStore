import { Router } from '@angular/router';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { map, catchError } from 'rxjs/operators';
import { HttpResponse } from '@angular/common/http';

import { RestDatasource } from '../../core/rest-datasource.service';
import { Url } from '../../models/url.model';
import { PageLink } from '../../models/enums/page-link.enum';
import { Login } from '../../models/login.model';

@Injectable({ providedIn: "root" })
export class AuthenticationService {
    constructor(
        private _router: Router,
        private _rest: RestDatasource) { }

    authenticated: boolean = false;
    name: string;
    password: string;
    callbackUrl: string;

    login(): Observable<boolean> {
        this.authenticated = false;
        let creds: Login = new Login(this.name, this.password);
        return this._rest.login(creds, Url.login)
            .pipe(
                catchError(e => {
                    this.authenticated = false;
                    return of(false);
                }),

                map((response: HttpResponse<{}>) => {
                    if (response.ok) {
                        this.authenticated = true;
                        this.password = null;
                        this._router.navigateByUrl(`/${PageLink.admin}`);
                    }
                    return this.authenticated;
                })
            );
    }

    logout(): void  {
        this.authenticated = false;
        this._rest.create<{}>(Url.logout, null).subscribe(response => { });
        this._router.navigateByUrl(`/${PageLink.store}`);
    }
}
