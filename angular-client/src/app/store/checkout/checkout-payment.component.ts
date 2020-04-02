import { Component, OnInit } from "@angular/core";

import { Router } from '@angular/router';
import { BaseFormComponent } from '../../models/components/base-form.model';
import { PaymentFormGroup } from '../../models/forms/payment-form.model';
import { nameof, createPageLink } from '../../core/helper-functions';
import { OrderService } from '../shared/order.service';
import { Payment } from '../../models/payment.model';
import { PageLink } from '../../models/enums/page-link.enum';
import { CustomFormControl } from '../../models/forms/custom-form-control.model';

@Component({
    templateUrl: './checkout-payment.component.html',
})
export class CheckoutPaymentComponent extends BaseFormComponent<PaymentFormGroup> implements OnInit {
    constructor(
        private _router: Router,
        private _orderService: OrderService) {

        super();
    }

    get numberCtrl(): CustomFormControl {
        return this.form.customControls[0];
    }

    get expiryCtrl(): CustomFormControl {
        return this.form.customControls[1];
    }

    get securityCtrl(): CustomFormControl {
        return this.form.customControls[2];
    }

    ngOnInit(): void {
                //this.form.controls['name'].valueChanges.subscribe((value: string) => {
        //    if (value.length == 4) {
        //        let newValue: string = `${value}-`;
        //        this.form.controls['name'].patchValue(newValue, { emitEvent: true });
        //    }
        //    //else {
        //    //    this.form.controls['name'].patchValue(oldValue, { emitEvent: false });
        //    //}

        //    console.log(this.form.controls['name'].value);
        //});
        this.form = new PaymentFormGroup(this._orderService.order);

        if (this._orderService.checkoutFormHasNotBeenFilled) {
            this._router.navigateByUrl(createPageLink(true, PageLink.store, PageLink.checkout));
        }
        else {
            this._orderService.storeSessionData();
        }
    }

    onSubmitForm(): void {
        if (this.form.valid) {
            this._orderService.cardNumber = this.form.get(nameof<Payment>("cardNumber")).value;
            this._orderService.cardExpiry = this.form.get(nameof<Payment>("cardExpiry")).value;
            this._orderService.cardSecurityCode = this.form.get(nameof<Payment>("cardSecurityCode")).value;

            this._router.navigateByUrl(createPageLink(true, PageLink.store, PageLink.summary));
        }
    }

    onGoToCheckout(): void {
        this._router.navigateByUrl(createPageLink(true, PageLink.store, PageLink.checkout));
    }
}
