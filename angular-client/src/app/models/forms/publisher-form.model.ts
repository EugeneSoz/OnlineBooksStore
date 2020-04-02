import { Validators } from "@angular/forms";

import { CustomFormGroup, CustomFormControl } from './custom-form-control.model';
import { PublisherDTO } from '../domain/DTO/publisherDTO.model';
import { ModelErrors } from '../validation/model-errors.model';
import { EntityType } from '../enums/entity-type.enum';
import { RangeValidator } from '../validation/range.formvalidator';
import { nameof } from '../../core/helper-functions';

export class PublisherFormGroup extends CustomFormGroup {
    constructor(
        publisher: PublisherDTO) {

        super();

        let nameRegExp: RegExp = new RegExp("^[A-Za-zА-Яа-я0-9_ ]+$");
        let countryRegExp: RegExp = new RegExp("^[A-Za-zА-Яа-я ]+$");
        this._me = new ModelErrors();

        this.addControl(nameof<PublisherDTO>("id"),
            new CustomFormControl(publisher.id,
                undefined,
                "ID",
                nameof<PublisherDTO>("id"),
                EntityType.Publisher,
                this._me));

        this.addControl(nameof<PublisherDTO>("name"),
            new CustomFormControl(publisher.name,
                Validators.compose([Validators.required, Validators.pattern(nameRegExp),
                RangeValidator.range(3, 100)]),
                "Название издательства",
                nameof<PublisherDTO>("name"),
                EntityType.Publisher,
                this._me));

        this.addControl(nameof<PublisherDTO>("country"),
            new CustomFormControl(publisher.country,
                Validators.compose([Validators.required, Validators.pattern(countryRegExp)]),
                "Страна нахождения издательства",
                nameof<PublisherDTO>("country"),
                EntityType.Publisher,
                this._me));
    }

    private _me: ModelErrors;
}
