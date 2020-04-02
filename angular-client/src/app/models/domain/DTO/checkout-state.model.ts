export class CheckoutState {
    constructor(
        public name: string,
        public address: string,
        public cardNumber: string,
        public cardExpiry: string,
        public cardSecurityCode: string) {}
}