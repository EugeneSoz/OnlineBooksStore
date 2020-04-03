import { Component, OnInit } from "@angular/core";

import { AdminSidebarSection } from "../../domain/model/components/admin-sidebar-section.model";
import { PageLink } from "../../domain/model/url/page-link.model";
import { createPageLink } from "../../infrastructure/helper-functions";

@Component({
    selector: "bs-admin-sidebar",
    templateUrl: "./admin-sidebar.component.html",
})
export class AdminSidebarComponent implements OnInit {
    sections: Array<AdminSidebarSection>;

    ngOnInit() {
        this.sections = new Array<AdminSidebarSection>();
        this.sections.push(new AdminSidebarSection(createPageLink(true, PageLink.admin), "Работа с бд"));

        this.sections.push(new AdminSidebarSection(
            createPageLink(true, PageLink.admin, PageLink.books), "Книги"));

        this.sections.push(new AdminSidebarSection(
            createPageLink(true, PageLink.admin, PageLink.categories), "Категории"));

        this.sections.push(new AdminSidebarSection(
            createPageLink(true, PageLink.admin, PageLink.publishers), "Издатели"));

        this.sections.push(new AdminSidebarSection(
            createPageLink(true, PageLink.admin, PageLink.orders), "Заказы"));

        this.sections.push(new AdminSidebarSection(createPageLink(true, PageLink.store), "Магазин"));
    }
}
