import { FormControl, ValidatorFn, FormGroup } from "@angular/forms";

import { EntityType } from '../enums/entity-type.enum';
import { ModelErrors } from '../validation/model-errors.model';

export class CustomFormControl extends FormControl {
    constructor(value: string | number | boolean, validator: ValidatorFn | ValidatorFn[],
        label: string, property: string,
        entityType: EntityType, me: ModelErrors) {

        super(value, validator);
        this.label = label;
        this.property = property;
        this._entityType = entityType;
        this._me = me;
    }

    label: string;
    property: string;
    private _entityType: EntityType;
    private _me: ModelErrors;

    getValidationMessages(): Array<string> {
        let messages: Array<string> = new Array<string>();

        if (this.errors) {
            for (let errorName in this.errors) {
                messages.push(this._me.getValidationErrors(this._entityType, this.property, errorName));
            }
        }

        return messages;
    }
}

export class CustomFormGroup extends FormGroup {
    constructor() {
        super({});
    }

    get customControls(): Array<CustomFormControl> {
        return Object.keys(this.controls)
            .map(k => this.controls[k] as CustomFormControl);
    }
}
