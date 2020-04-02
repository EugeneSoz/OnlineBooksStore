import { Component } from '@angular/core';
import { StoreService } from './shared/store.service';
import { CartService } from './shared/cart.service';
import { OrderService } from './shared/order.service';

@Component({
    templateUrl: './store.component.html',
    providers: [
        StoreService,
        CartService,
        OrderService
    ]
})
export class StoreComponent { }
