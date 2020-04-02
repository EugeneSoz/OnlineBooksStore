import { Component, Input } from '@angular/core';

import { CartService } from '../shared/cart.service';
import { BookResponse } from '../../models/domain/DTO/book-response.model';
import { createPageLink } from '../../core/helper-functions';
import { PageLink } from '../../models/enums/page-link.enum';

@Component({
  selector: 'bs-book-card',
  templateUrl: './book-card.component.html',
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
