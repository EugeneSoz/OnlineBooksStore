import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";

import { AuthenticationComponent } from "./authentication/authentication.component";
import { PageLink } from "../domain/model/url/page-link.model";

const routes: Routes = [
    { path: PageLink.login, component: AuthenticationComponent },
    {
        path: "", redirectTo: `/${PageLink.store}`, pathMatch: "full"
    },
    {
        path: PageLink.store,
        loadChildren: () => import("./store/store.module").then(m => m.StoreModule)
    },
    {
        path: PageLink.admin,
        loadChildren: () => import("./admin/admin.module").then(m => m.AdminModule)
    }
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AppRoutingModule { }
