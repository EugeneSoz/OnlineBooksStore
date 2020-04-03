import { Component, OnInit } from "@angular/core";
import { Router } from "@angular/router";

import { OrderService } from "../shared/order.service";
import { BaseFormComponent } from "../../../domain/model/components/base-form.model";
import { CheckoutFormGroup } from "../../../domain/model/forms/checkout-form.model";
import { PageLink } from "../../../domain/model/url/page-link.model";
import { Order } from "../../../domain/model/entities/order.model";
import { createPageLink, nameof } from "../../../infrastructure/helper-functions";

@Component({
    templateUrl: "./checkout-details.component.html",
})
export class CheckoutDetailsComponent extends BaseFormComponent<CheckoutFormGroup> implements OnInit {
    constructor(
        private _router: Router,
        private _orderService: OrderService) {

        super();
    }

    ngOnInit(): void {
        this.form = new CheckoutFormGroup(this._orderService.order);
         if (this._orderService.orderHasNotBeenCreated) {
             this._router.navigateByUrl(createPageLink(true, PageLink.store, PageLink.cart));
         }       
    }

    onSubmitForm(): void {
        if (this.form.valid) {
            this._orderService.order.name = this.form.get(nameof<Order>("name")).value;
            this._orderService.order.address = this.form.get(nameof<Order>("address")).value;

            this._router.navigateByUrl(createPageLink(true, PageLink.store, PageLink.payment));
        }
    }

    onGoToStore(): void {
        this._router.navigateByUrl(createPageLink(true, PageLink.store));
    }
}
