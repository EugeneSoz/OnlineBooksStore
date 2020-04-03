export class OrderConfirmation {
    constructor(
        public orderId: number = 0,
        public authCode: string = "",
        public amount: number = 0) { }
}
