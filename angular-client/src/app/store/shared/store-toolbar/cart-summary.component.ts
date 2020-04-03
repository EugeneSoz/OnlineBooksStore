import { Component } from "@angular/core";
import { CartService } from "../../shared/cart.service";
import { createPageLink } from "../../../../infrastructure/helper-functions";
import { PageLink } from "../../../../domain/model/url/page-link.model";

@Component({
    selector: "bs-cart-summary",
    templateUrl: "./cart-summary.component.html",
})
export class CartSummaryComponent {
    constructor(
        private _cart: CartService) {
        this.pageLink = createPageLink(true, PageLink.store, PageLink.cart);
    }

    pageLink: string;
    get itemsCount(): number {
        return this._cart.itemCount;
    }

    get totalPrice(): number {
        return this._cart.totalPrice;
    }
}
