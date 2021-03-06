import { Component, OnInit } from "@angular/core";
import { Router } from "@angular/router";

import { OrderService } from "../shared/order.service";
import { OrderConfirmation } from "../../../domain/model/order.confirmation.model";
import { PageLink } from "../../../domain/model/url/page-link.model";
import { createPageLink } from "../../../infrastructure/helper-functions";

@Component({
    templateUrl: "./order-confirmation.component.html",
})
export class OrderConfirmationComponent implements OnInit {
    constructor(
        private _router: Router,
        private _orderService: OrderService) {
    }

    get orderConfirmation(): OrderConfirmation {
        return this._orderService.orderConfirmation;
    }

    get orderHasBeenPlaced(): boolean {
        return this._orderService.orderHasBeenPlaced;
    }

    ngOnInit(): void {
        if (!this._orderService.orderSubmitted) {
            this._router.navigateByUrl(createPageLink(true, PageLink.store, PageLink.summary));
        }
        else {
            this._orderService.placeOrder();
        }
    }

    onGoToStore(): void {
        this._orderService.orderConfirmation = new OrderConfirmation();
        this._router.navigateByUrl(`/${PageLink.store}`);
    }
}
