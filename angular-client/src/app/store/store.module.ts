import { NgModule } from "@angular/core";
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { StoreRoutingModule } from './store-routing.module';

import { CollapseModule } from 'ngx-bootstrap/collapse';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';

import { SharedModule } from '../shared/shared.module';
import { BookCardComponent } from './books-list/book-card.component';
import { BookDetailComponent } from './book-detail/book-detail.component';
import { BooksGridComponent } from './books-list/books-grid.component';
import { ActionsComponent } from './shared/store-toolbar/actions.component';
import { SearchToolbarComponent } from './shared/store-toolbar/search-toolbar.component';
import { CartSummaryComponent } from './shared/store-toolbar/cart-summary.component';
import { StoreToolbarComponent } from './shared/store-toolbar/store-toolbar.component';
import { StoreComponent } from './store.component';
import { StoreSidebarComponent } from './store-sidebar.component';
import { EmptyStoreToolbarComponent } from './shared/empty-store-toolbar/empty-toolbar.component';
import { CheckoutSummaryComponent } from './checkout/checkout-summary.component';
import { CheckoutDetailsComponent } from './checkout/checkout-details.component';
import { CartDetailsComponent } from './cart-detail/cart-details.component';
import { CheckoutPaymentComponent } from './checkout/checkout-payment.component';
import { OrderConfirmationComponent } from './checkout/order-confirmation.component';
import { BooksListComponent } from './books-list/books-list.component';
import { UpperCaseDirective } from './shared/uppercase.directive';
import { CardExpiryDirective } from './shared/card-expiry.directive';
import { CardNumberDirective } from './shared/card-number.directive';


@NgModule({
    imports: [
        CommonModule,
        ReactiveFormsModule,
        StoreRoutingModule,
        SharedModule,
        CollapseModule.forRoot(),
        BsDropdownModule.forRoot(),
    ],
    exports: [],
    declarations: [
        BookCardComponent,
        BookDetailComponent,
        BooksGridComponent,
        BooksListComponent,
        ActionsComponent,
        CartSummaryComponent,
        StoreToolbarComponent,
        SearchToolbarComponent,
        StoreComponent,
        StoreSidebarComponent,
        EmptyStoreToolbarComponent,
        CartDetailsComponent,
        CheckoutDetailsComponent,
        CheckoutPaymentComponent,
        CheckoutSummaryComponent,
        OrderConfirmationComponent,
        UpperCaseDirective,
        CardExpiryDirective,
        CardNumberDirective
    ],
    providers: [
    ],
})
export class StoreModule { }
