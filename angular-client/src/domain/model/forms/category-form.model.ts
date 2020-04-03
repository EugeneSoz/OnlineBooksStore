import { Validators } from "@angular/forms";

import { CustomFormGroup, CustomFormControl } from "./custom-form-control.model";
import { ModelErrors } from "../validation/model-errors.model";
import { EntityType } from "../enums/entity-type.enum";
import { RangeValidator } from "../validation/range.formvalidator";
import { nameof } from "../../../infrastructure/helper-functions";
import { CategoryDTO } from "../entities/DTO/categoryDTO.model";

export class CategoryFormGroup extends CustomFormGroup {
    constructor(
        category: CategoryDTO) {

        super();

        let nameRegExp: RegExp = new RegExp("^[A-Za-zА-Яа-я0-9_ ]+$");
        this._me = new ModelErrors();

        this.addControl(nameof<CategoryDTO>("id"),
            new CustomFormControl(category.id,
                undefined,
                "ID",
                nameof<CategoryDTO>("id"),
                EntityType.Category,
                this._me));

        this.addControl(nameof<CategoryDTO>("parentCategoryID"),
            new CustomFormControl(category.parentCategoryID,
                undefined,
                "Родительская категория",
                nameof<CategoryDTO>("parentCategoryID"),
                EntityType.Category,
                this._me));


        this.addControl(nameof<CategoryDTO>("name"),
            new CustomFormControl(category.name,
                Validators.compose([Validators.required, Validators.pattern(nameRegExp),
                RangeValidator.range(3, 100)]),
                "Название категории",
                nameof<CategoryDTO>("name"),
                EntityType.Category,
                this._me));

    }

    private _me: ModelErrors;
}
