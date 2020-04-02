import { Component } from '@angular/core';
import { CartService } from '../../shared/cart.service';
import { createPageLink } from '../../../core/helper-functions';
import { PageLink } from '../../../models/enums/page-link.enum';

@Component({
    selector: 'bs-cart-summary',
    templateUrl: './cart-summary.component.html',
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
