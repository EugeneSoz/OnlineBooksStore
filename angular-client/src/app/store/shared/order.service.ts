import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { OrderLine } from '../../models/order-line.model';
import { CartService } from './cart.service';
import { Order } from '../../models/domain/order.model';
import { Url } from '../../models/url.model';
import { RestDatasource } from '../../core/rest-datasource.service';
import { OrderConfirmation } from '../../models/order.confirmation.model';
import { CheckoutState } from '../../models/domain/DTO/checkout-state.model';

@Injectable()
export class OrderService {
    constructor(
        private _cart: CartService,
        private _rest: RestDatasource) {
        this.getSessionData().subscribe(data => {
            if (data != null) {
                this.order.name = data.name;
                this.order.address = data.address;
                this.cardNumber = data.cardNumber;
                this.cardExpiry = data.cardExpiry;
                this.cardSecurityCode = data.cardSecurityCode;
            }
        })
    }

    orderSubmitted: boolean = false;
    order: Order = new Order();
    orderConfirmation: OrderConfirmation = new OrderConfirmation();

    get orderHasNotBeenCreated(): boolean {
        return this.order.goods.length > 0 ? false : true;
    }

    get checkoutFormHasNotBeenFilled(): boolean {
        return this.order.name == "" || this.order.address == "" ? true : false;
    }

    get paymentFormHasNotBeenFilled(): boolean {
        return this.order.payment.cardNumber == null
            || this.order.payment.cardExpiry == null
            || this.order.payment.cardSecurityCode == null
            ? true
            : false;
    }

    get orderHasBeenPlaced(): boolean {
        return this.orderConfirmation.orderId != 0
            && this.orderConfirmation.amount != 0
            && this.orderConfirmation.authCode != ""
            ? true
            : false;
    }

    set cardNumber(value: string) {
        this.order.payment.cardNumber = value;
    }

    set cardExpiry(value: string) {
        this.order.payment.cardExpiry = value;
    }

    set cardSecurityCode(value: string) {
        this.order.payment.cardSecurityCode = value;
    }

    createOrder(): void {
        this.order.goods = this._cart.lines
            .map(p => new OrderLine(p.itemId, p.quantity));
    }

    getOrders(): Observable<Array<Order>> {
        return this._rest.getAll<Array<Order>>(Url.orders);
    }

    private getSessionData(): Observable<CheckoutState> {
        return this._rest.getAll<CheckoutState>(Url.checkout_session);
    }

    storeSessionData(): void {
        let state: CheckoutState = new CheckoutState(this.order.name, this.order.address,
            this.order.payment.cardNumber, this.order.payment.cardExpiry, this.order.payment.cardSecurityCode);

        this._rest.receiveAll<void, CheckoutState>(Url.checkout_session, state)
            .subscribe(response => { });
    }

    placeOrder(): void {
        this._rest.receiveAll<OrderConfirmation, Order>(Url.orders, this.order)
            .subscribe(response => {
                this.orderConfirmation = response;
                this.clear();
            });
    }

    shipOrder(order: Order): Observable<void> {
        return this._rest.getOne<void>(`${Url.orders}/${order.orderId}`);
    }

    private clear(): void {
        this.order = new Order();
        this._cart.clear();
        this.orderSubmitted = false;
    }
}
