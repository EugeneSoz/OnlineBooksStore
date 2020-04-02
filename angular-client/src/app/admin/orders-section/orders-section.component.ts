import { Component, OnInit } from "@angular/core";

import { Order } from '../../models/domain/order.model';
import { OrderService } from '../../store/shared/order.service';
import { CartService } from '../../store/shared/cart.service';

@Component({
    templateUrl: './orders-section.component.html',
    providers: [
        OrderService,
        CartService
    ]
})
export class OrderSectionComponent implements OnInit {
    constructor(
        private _orderService: OrderService) { }

    orders: Array<Order> = null;

    ngOnInit(): void {
        this.getOrders();
    }

    private getOrders(): void {
        this._orderService.getOrders()
            .subscribe(response => this.orders = response);
    }

    markShipped(order: Order) {
        //this._cart.shipOrder(order);
    }
}
