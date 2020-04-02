import { NgModule } from "@angular/core";
import { Routes, RouterModule } from '@angular/router';

import { PageLink } from '../models/enums/page-link.enum';
import { StoreComponent } from './store.component';
import { BookDetailComponent } from './book-detail/book-detail.component';
import { CartDetailsComponent } from './cart-detail/cart-details.component';
import { CheckoutDetailsComponent } from './checkout/checkout-details.component';
import { CheckoutPaymentComponent } from './checkout/checkout-payment.component';
import { CheckoutSummaryComponent } from './checkout/checkout-summary.component';
import { OrderConfirmationComponent } from './checkout/order-confirmation.component';
import { BooksListComponent } from './books-list/books-list.component';


const routes: Routes = [
    {
        path: "", component: StoreComponent,
        children: [
            { path: `${PageLink.detail}/:id`, component: BookDetailComponent },
            { path: PageLink.checkout, component: CheckoutDetailsComponent },
            { path: PageLink.payment, component: CheckoutPaymentComponent },
            { path: PageLink.summary, component: CheckoutSummaryComponent },
            { path: PageLink.confirmation, component: OrderConfirmationComponent },
            { path: PageLink.cart, component: CartDetailsComponent },
            { path: "", component: BooksListComponent },
        ]
    },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class StoreRoutingModule { }
