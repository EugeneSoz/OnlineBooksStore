import { Component, OnInit } from "@angular/core";

import { Router } from "@angular/router";
import { OrderService } from "../shared/order.service";
import { BaseFormComponent } from "../../../domain/model/components/base-form.model";
import { PaymentFormGroup } from "../../../domain/model/forms/payment-form.model";
import { CustomFormControl } from "../../../domain/model/forms/custom-form-control.model";
import { PageLink } from "../../../domain/model/url/page-link.model";
import { Payment } from "../../../domain/model/payment.model";
import { createPageLink, nameof } from "../../../infrastructure/helper-functions";

@Component({
    templateUrl: "./checkout-payment.component.html",
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
