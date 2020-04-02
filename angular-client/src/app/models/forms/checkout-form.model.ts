import { Validators } from '@angular/forms';

import { CustomFormGroup, CustomFormControl } from './custom-form-control.model';
import { Order } from '../domain/order.model';
import { ModelErrors } from '../validation/model-errors.model';
import { EntityType } from '../enums/entity-type.enum';
import { nameof } from '../../core/helper-functions';

export class CheckoutFormGroup extends CustomFormGroup {
    constructor(
        order: Order) {

        super();
        let nameRegExp: RegExp = new RegExp("^[A-Za-zА-Яа-я ]+$");
        let addressRegExp: RegExp = new RegExp("^[A-Za-zА-Яа-я .]+$");
        this._me = new ModelErrors();
        this.addControl(nameof<Order>("name"),
            new CustomFormControl(order.name,
                Validators.compose([Validators.required, Validators.pattern(nameRegExp)]),
                "Ваше имя",
                nameof<Order>("name"),
                EntityType.Order,
                this._me));

        this.addControl(nameof<Order>("address"),
            new CustomFormControl(order.address,
                Validators.compose([Validators.required, Validators.pattern(addressRegExp)]),
                "Адрес доставки",
                nameof<Order>("address"),
                EntityType.Order,
                this._me));
    }

    private _me: ModelErrors;
}
