import { Payment } from '../payment.model';
import { OrderLine } from '../order-line.model';

export class Order {
    constructor(
        public orderId: number = 0,
        public name: string = "",
        public address: string = "",
        public payment: Payment = null,
        public shipped: boolean = false,
        public goods: Array<OrderLine> = null) {

        this.payment = new Payment();
        this.goods = new Array<OrderLine>();
    }
}
