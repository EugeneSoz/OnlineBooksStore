import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { StoreService } from '../shared/store.service';
import { BookResponse } from '../../models/domain/DTO/book-response.model';
import { CartService } from '../shared/cart.service';
import { PageLink } from '../../models/enums/page-link.enum';

@Component({
    templateUrl: './book-detail.component.html',
})
export class BookDetailComponent implements OnInit {
    constructor(
        private _storeService: StoreService,
        private _activeRoute: ActivatedRoute,
        private _cartService: CartService) { }

    private _id: number = 0;

    book: BookResponse = null;
    storePageLink: string = `/${PageLink.store}`;

    ngOnInit(): void {
        this._id = Number.parseInt(this._activeRoute.snapshot.paramMap.get("id")) || 0;
        this.getBook();
    }

    onAddToCart(): void {
        this._cartService.addToCart(this.book);
    }

    private getBook(): void {
        this._storeService.getBook(this._id)
            .subscribe(result => this.book = result);
    }
}
