import { Component, OnInit } from "@angular/core";
import { Router } from '@angular/router';

import { Order } from '../../models/domain/order.model';
import { CartService } from '../shared/cart.service';
import { PageLink } from '../../models/enums/page-link.enum';
import { createPageLink } from '../../core/helper-functions';
import { OrderService } from '../shared/order.service';

@Component({
    templateUrl: './checkout-summary.component.html',
})
export class CheckoutSummaryComponent implements OnInit {
    constructor(
        private _router: Router,
        private _orderService: OrderService,
        public cart: CartService) {
    }

    get order(): Order {
        return this._orderService.order;
    }

    ngOnInit(): void {
        if (this._orderService.paymentFormHasNotBeenFilled) {
            this._router.navigateByUrl(createPageLink(true, PageLink.store, PageLink.payment));
        }
    }

    onSubmitOrder() {
        this._orderService.orderSubmitted = true;
        this._router.navigateByUrl(createPageLink(true, PageLink.store, PageLink.confirmation));
    }

    onGoToPayment(): void {
        this._router.navigateByUrl(createPageLink(true, PageLink.store, PageLink.payment));
    }
}
