import { NgModule } from "@angular/core";
import { CommonModule } from '@angular/common';

import { AdminRoutingModule } from './admin-rooting.module';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';

import { CollapseModule } from 'ngx-bootstrap/collapse';
import { TooltipModule } from 'ngx-bootstrap/tooltip';
import { TypeaheadModule } from 'ngx-bootstrap/typeahead';
import { ModalModule } from 'ngx-bootstrap/modal';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { SharedModule } from '../shared/shared.module';
import { RelatedBookTableComponent } from './shared/related-books/related-books-table.component';
import { AdminSidebarComponent } from './admin-sidebar.component';
import { PublishersTableComponent } from './publishers-section/publishers-table.component';
import { PublisherFormComponent } from './publishers-section/publisher-form.component';
import { PublishersSectionComponent } from './publishers-section/publishers-section.component';
import { CategoryFormComponent } from './categories-section/category-form.component';
import { CategoriesTableComponent } from './categories-section/categories-table.component';
import { CategoriesSectionComponent } from './categories-section/categories-section.component';
import { BooksSectionComponent } from './books-section/books-section.component';
import { BooksTableComponent } from './books-section/books-table.component';
import { BookFormComponent } from './books-section/book-form.component';
import { BookInfoComponent } from './books-section/book-info.component';
import { AdminComponent } from './admin.component';
import { AdminFilterComponent } from './shared/admin-filter/admin-filter.component';
import { AdminToolbarComponent } from './shared/admin-toolbar/admin-toolbar.component';
import { TableHeadComponent } from './shared/table-head/table-head.component';
import { OrderSectionComponent } from './orders-section/orders-section.component';
import { HomeComponent } from './home/home.component';


@NgModule({
    imports: [
        CommonModule,
        AdminRoutingModule,
        SharedModule,
        FormsModule,
        ReactiveFormsModule,
        CollapseModule.forRoot(),
        TooltipModule.forRoot(),
        ModalModule.forRoot(),
        TypeaheadModule.forRoot(),
        TabsModule.forRoot(),
    ],
    declarations: [
        RelatedBookTableComponent,
        AdminSidebarComponent,

        PublishersTableComponent,
        PublisherFormComponent,
        PublishersSectionComponent,

        CategoryFormComponent,
        CategoriesTableComponent,
        CategoriesSectionComponent,

        BooksSectionComponent,
        BooksTableComponent,
        BookFormComponent,
        BookInfoComponent,

        AdminComponent,
        AdminFilterComponent,
        AdminToolbarComponent,
        TableHeadComponent,
        HomeComponent,
        OrderSectionComponent,
    ],
    exports: [],
    providers: [],
})
export class AdminModule { }
