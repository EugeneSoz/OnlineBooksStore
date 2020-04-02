import { NgModule } from "@angular/core";
import { CommonModule } from '@angular/common';

import { PaginationComponent } from './pagination/pagination.component';
import { ValidationComponent } from './validation/validation.component';
import { ServerValidationComponent } from './server-validation/server-validation.component';

@NgModule({
    imports: [
        CommonModule,
    ],
    exports: [
        PaginationComponent,
        ValidationComponent,
        ServerValidationComponent,
    ],
    declarations: [
        PaginationComponent,
        ValidationComponent,
        ServerValidationComponent,
    ],
    providers: [],
})
export class SharedModule { }
