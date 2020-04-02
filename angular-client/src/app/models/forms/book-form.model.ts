import { Validators } from '@angular/forms';

import { CustomFormGroup, CustomFormControl } from './custom-form-control.model';
import { ModelErrors } from '../validation/model-errors.model';
import { BookDTO } from '../domain/DTO/bookDTO.model';
import { EntityType } from '../enums/entity-type.enum';
import { RangeValidator } from '../validation/range.formvalidator';
import { NotNullMinValidator } from '../validation/notNullMin.formvalidator';
import { nameof } from '../../core/helper-functions';

export class BookFormGroup extends CustomFormGroup {
    constructor(
        book: BookDTO) {

        super();

        let titleRegExp: RegExp = new RegExp("^[A-Za-zА-Яа-я0-9_ ]+$");
        let authorsRegExp: RegExp = new RegExp("^[A-Za-zА-Яа-я .]+$");
        let languageRegExp: RegExp = new RegExp("^[A-Za-zА-Яа-я]+$");
        let yearRegExp: RegExp = new RegExp("^[1-9][0-9][0-9][0-9]$");
        let pageRegExp: RegExp = new RegExp("^[1-9][0-9]*$");
        let priceRegExp: RegExp = new RegExp("^[1-9][0-9]*\.?[0-9]?[0-9]?$");
        this._me = new ModelErrors();
        this.addControl(nameof<BookDTO>("id"),
            new CustomFormControl(book.id,
                undefined,
                "ID",
                nameof<BookDTO>("id"),
                EntityType.Book,
                this._me));

        this.addControl(nameof<BookDTO>("title"),
            new CustomFormControl(book.title,
                Validators.compose([Validators.required, Validators.pattern(titleRegExp),
                RangeValidator.range(3, 100)]),
                "Название книги",
                nameof<BookDTO>("title"),
                EntityType.Book,
                this._me));

        this.addControl(nameof<BookDTO>("authors"),
            new CustomFormControl(book.authors,
                Validators.compose([Validators.required, Validators.pattern(authorsRegExp)]),
                "Авторы",
                nameof<BookDTO>("authors"),
                EntityType.Book,
                this._me));


        this.addControl(nameof<BookDTO>("language"),
            new CustomFormControl(book.language,
                Validators.compose([Validators.required, Validators.pattern(languageRegExp)]),
                "Язык",
                nameof<BookDTO>("language"),
                EntityType.Book,
                this._me));

        this.addControl(nameof<BookDTO>("year"),
            new CustomFormControl(book.year,
                Validators.compose([Validators.required, Validators.pattern(yearRegExp)]),
                "Дата публикации",
                nameof<BookDTO>("year"),
                EntityType.Book,
                this._me));


        this.addControl(nameof<BookDTO>("pageCount"),
            new CustomFormControl(book.pageCount,
                Validators.compose([Validators.required, Validators.pattern(pageRegExp)]),
                "Количество сраниц",
                nameof<BookDTO>("pageCount"),
                EntityType.Book,
                this._me));

        this.addControl(nameof<BookDTO>("price"),
            new CustomFormControl(book.price,
                Validators.compose([Validators.required, Validators.pattern(priceRegExp)]),
                "Цена",
                nameof<BookDTO>("price"),
                EntityType.Book,
                this._me));

        this.addControl(nameof<BookDTO>("categoryID"),
            new CustomFormControl(book.categoryID,
                NotNullMinValidator.notNullMin(1),
                "Категория",
                nameof<BookDTO>("categoryID"),
                EntityType.Book,
                this._me));

        this.addControl(nameof<BookDTO>("publisherID"),
            new CustomFormControl(book.publisherID,
                NotNullMinValidator.notNullMin(1),
                "Издательство",
                nameof<BookDTO>("publisherID"),
                EntityType.Book,
                this._me));

        this.addControl(nameof<BookDTO>("description"),
            new CustomFormControl(book.description,
                Validators.compose([Validators.required, RangeValidator.range(3, 1000)]),
                "Описание",
                nameof<BookDTO>("description"),
                EntityType.Book,
                this._me));

        this.addControl(nameof<BookDTO>("bookCover"),
            new CustomFormControl(book.description,
                undefined,
                "Обложка книги",
                nameof<BookDTO>("bookCover"),
                EntityType.Book,
                this._me));
    }

    private _me: ModelErrors;
}
