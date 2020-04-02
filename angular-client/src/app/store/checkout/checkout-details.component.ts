import { Component, OnInit } from "@angular/core";
import { Router } from '@angular/router';

import { BaseFormComponent } from '../../models/components/base-form.model';
import { CheckoutFormGroup } from '../../models/forms/checkout-form.model';
import { Order } from '../../models/domain/order.model';
import { nameof, createPageLink } from '../../core/helper-functions';
import { OrderService } from '../shared/order.service';
import { PageLink } from '../../models/enums/page-link.enum';

@Component({
    templateUrl: './checkout-details.component.html',
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
