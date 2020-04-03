import { Component, Input } from "@angular/core";

import { CartService } from "../shared/cart.service";
import { createPageLink } from "../../../infrastructure/helper-functions";
import { PageLink } from "../../../domain/model/url/page-link.model";
import { BookResponse } from "../../../domain/model/entities/DTO/book-response.model";

@Component({
  selector: "bs-book-card",
  templateUrl: "./book-card.component.html",
})
export class BookCardComponent {
    constructor(
        private _cartService: CartService) {
        this.pageLink = createPageLink(true, PageLink.store, PageLink.detail);
    }

    @Input() book: BookResponse = null;

    pageLink: string;

    onAddToCart(): void {
        this._cartService.addToCart(this.book);
    }
}
