import { Component, OnInit } from "@angular/core";
import { ActivatedRoute } from "@angular/router";

import { StoreService } from "../shared/store.service";
import { CartService } from "../shared/cart.service";
import { BookResponse } from "../../../domain/model/entities/DTO/book-response.model";
import { PageLink } from "../../../domain/model/url/page-link.model";

@Component({
    templateUrl: "./book-detail.component.html",
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
