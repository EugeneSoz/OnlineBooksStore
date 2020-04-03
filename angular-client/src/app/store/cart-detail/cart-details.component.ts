import { Component } from "@angular/core";
import { Router } from "@angular/router";

import { CartService } from "../shared/cart.service";
import { OrderService } from "../shared/order.service";
import { PageLink } from "../../../domain/model/url/page-link.model";
import { createPageLink } from "../../../infrastructure/helper-functions";

@Component({
    templateUrl: "./cart-details.component.html",
})
export class CartDetailsComponent {
    constructor(
        public cart: CartService,
        private _orderService: OrderService,
        private _router: Router) { }

    storePageLink: string = `/${PageLink.store}`;

    get isCartEmpty(): boolean {
        return this.cart.itemCount == 0 ? true : false;
    }

    onUpdate(bookId: number, quantity: string): void {
        let q = Number.parseInt(quantity);
        this.cart.updateQuantity(bookId, q);
    }

    onPlaceAnOrder(): void {
        let checkoutPageLink = createPageLink(true, PageLink.store, PageLink.checkout);
        this._orderService.createOrder();
        this._router.navigate([checkoutPageLink]);
    }
}
