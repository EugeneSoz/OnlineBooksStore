import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { PageLink } from './models/enums/page-link.enum';
import { AuthenticationComponent } from './authentication/authentication.component';

const routes: Routes = [
    { path: PageLink.login, component: AuthenticationComponent },
    {
        path: "", redirectTo: `/${PageLink.store}`, pathMatch: "full"
    },
    {
        path: PageLink.store,
        loadChildren: () => import('./store/store.module').then(m => m.StoreModule)
    },
    {
        path: PageLink.admin,
        loadChildren: () => import('./admin/admin.module').then(m => m.AdminModule)
    },
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AppRoutingModule { }
