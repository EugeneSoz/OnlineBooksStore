import { NgModule } from "@angular/core";
import { Routes, RouterModule } from '@angular/router';

import { PageLink } from '../models/enums/page-link.enum';
import { AdminComponent } from './admin.component';
import { HomeComponent } from './home/home.component';
import { BooksSectionComponent } from './books-section/books-section.component';
import { CategoriesSectionComponent } from './categories-section/categories-section.component';
import { PublishersSectionComponent } from './publishers-section/publishers-section.component';
import { OrderSectionComponent } from './orders-section/orders-section.component';
import { BooksTableComponent } from './books-section/books-table.component';
import { BookFormComponent } from './books-section/book-form.component';
import { CategoriesTableComponent } from './categories-section/categories-table.component';
import { CategoryFormComponent } from './categories-section/category-form.component';
import { PublishersTableComponent } from './publishers-section/publishers-table.component';
import { PublisherFormComponent } from './publishers-section/publisher-form.component';
import { AuthenticationGuard } from '../authentication/shared/authentication.guard';

const booksChild: Routes = [
    { path: ":mode/:id", component: BookFormComponent },
    { path: ":mode", component: BookFormComponent },
    { path: "", component: BooksTableComponent }
];

const categoriesChild: Routes = [
    { path: ":mode/:id", component: CategoryFormComponent },
    { path: ":mode", component: CategoryFormComponent },
    { path: "", component: CategoriesTableComponent }
];

const publishersChild: Routes = [
    { path: ":mode/:id", component: PublisherFormComponent },
    { path: ":mode", component: PublisherFormComponent },
    { path: "", component: PublishersTableComponent },
];

const routes: Routes = [
    {
        path: "", component: AdminComponent,
        canActivateChild: [AuthenticationGuard],
        children: [
            { path: "", component: HomeComponent },
            {
                path: PageLink.books, component: BooksSectionComponent,
                children: booksChild
            },
            {
                path: PageLink.categories, component: CategoriesSectionComponent,
                children: categoriesChild
            },
            {
                path: PageLink.publishers, component: PublishersSectionComponent,
                children: publishersChild
            },
            { path: PageLink.orders, component: OrderSectionComponent }
        ]
    },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class AdminRoutingModule { }
