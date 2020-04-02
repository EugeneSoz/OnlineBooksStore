import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';

import { AuthenticationComponent } from './authentication.component';

@NgModule({
    imports: [
        CommonModule,
        RouterModule,
        FormsModule
    ],
    exports: [AuthenticationComponent],
    declarations: [AuthenticationComponent],
    providers: [],
})
export class AuthenticationModule { }
