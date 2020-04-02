import { EntityType } from '../enums/entity-type.enum';
import { Publisher } from '../domain/publisher.model';
import { ErrorAttributes } from '../enums/error-attributes.enum';
import { Category } from '../domain/category.model';
import { Book } from '../domain/book.model';
import { Order } from '../domain/order.model';
import { nameof } from '../../core/helper-functions';
import { Payment } from '../payment.model';

export class ModelErrors {

    getValidationErrors(entityType: EntityType, property: string,
        errorAttribute: string): string {
        switch (entityType) {
            case EntityType.Publisher:
                return this.getPublishersValidationErrors(property, errorAttribute);
            case EntityType.Category:
                return this.getCategoriesValidationErrors(property, errorAttribute);
            case EntityType.Book:
                return this.getBooksValidationErrors(property, errorAttribute);
            case EntityType.Order:
                return this.getOrdersValidationErrors(property, errorAttribute);
        }
    }

    private getPublishersValidationErrors(property: string, errorAttribute: string): string {
        let msg: string = null;

        if (property == nameof<Publisher>("name")
            && errorAttribute == ErrorAttributes.required) {
            msg = "Укажите название издательства";
        }
        else if (property == nameof<Publisher>("name")
            && errorAttribute == ErrorAttributes.pattern) {
            msg = "Неверный формат ввода издательства";
        }
        else if (property == nameof<Publisher>("name")
            && errorAttribute == ErrorAttributes.range) {
            msg = "Название должно быть не меньше 2 и не больше 100 символов";
        }
        else if (property == nameof<Publisher>("country")
            && errorAttribute == ErrorAttributes.required) {
            msg = "Укажите название страны нахождения издательства";
        }
        else if (property == nameof<Publisher>("country")
            && errorAttribute == ErrorAttributes.pattern) {
            msg = "Неверный формат ввода страны нахождения издательства";
        }
        else if (property == nameof<Publisher>("country")
            && errorAttribute == ErrorAttributes.range) {
            msg = "Название должно быть не меньше 2 и не больше 100 символов";
        }

        return msg;
    }

    private getCategoriesValidationErrors(property: string, errorAttribute: string): string {
        let msg: string = null;

        if (property == nameof<Category>("name")
            && errorAttribute == ErrorAttributes.required) {
            msg = "Укажите название категории/подкатегории";
        }
        else if (property == nameof<Category>("name")
            && errorAttribute == ErrorAttributes.pattern) {
            msg = "Неверный формат ввода категории/подкатегории";
        }
        else if (property == nameof<Category>("name")
            && errorAttribute == ErrorAttributes.range) {
            msg = "Название должно быть не меньше 2 и не больше 100 символов";
        }

        return msg;
    }

    private getBooksValidationErrors(property: string, errorAttribute: string): string {
        let msg: string = null;

        if (property == nameof<Book>("title")
            && errorAttribute == ErrorAttributes.required) {
            msg = "Укажите название книги";
        }
        else if (property == nameof<Book>("title")
            && errorAttribute == ErrorAttributes.pattern) {
            msg = "Неверный формат название книги";
        }
        else if (property == nameof<Book>("title")
            && errorAttribute == ErrorAttributes.range) {
            msg = "Название должно быть не меньше 2 и не больше 250 символов";
        }
        else if (property == nameof<Book>("authors")
            && errorAttribute == ErrorAttributes.required) {
            msg = "Укажите автора или авторов";
        }
        else if (property == nameof<Book>("authors")
            && errorAttribute == ErrorAttributes.pattern) {
            msg = "Неверный формат ввода автора или авторов";
        }
        else if (property == nameof<Book>("language")
            && errorAttribute == ErrorAttributes.required) {
            msg = "Укажите язык книги";
        } else if (property == nameof<Book>("language")
            && errorAttribute == ErrorAttributes.pattern) {
            msg = "Неверный формат ввода языка книги";
        }
        else if (property == nameof<Book>("year")
            && errorAttribute == ErrorAttributes.required) {
            msg = "Укажите год издания книги в правильном формате";
        }
        else if (property == nameof<Book>("year")
            && errorAttribute == ErrorAttributes.pattern) {
            msg = "Укажите год издания книги в правильном формате";
        }
        else if (property == nameof<Book>("pageCount")
            && errorAttribute == ErrorAttributes.required) {
            msg = "Укажите количество страниц в правильном формате";
        }
        else if (property == nameof<Book>("pageCount")
            && errorAttribute == ErrorAttributes.pattern) {
            msg = "Укажите количество страниц в правильном формате";
        }
        else if (property == nameof<Book>("price")
            && errorAttribute == ErrorAttributes.required) {
            msg = "Укажите цену книги в правильном формате";
        }
        else if (property == nameof<Book>("price")
            && errorAttribute == ErrorAttributes.pattern) {
            msg = "Укажите цену книги в правильном формате";
        }
        if (property == nameof<Book>("description")
            && errorAttribute == ErrorAttributes.required) {
            msg = "Добавьте какое либо описание книги";
        }
        else if (property == nameof<Book>("description")
            && errorAttribute == ErrorAttributes.range) {
            msg = "Название должно быть не меньше 2 и не больше 1000 символов";
        }
        else if (property == nameof<Book>("categoryID")
            && errorAttribute == ErrorAttributes.notNullMin) {
            msg = "Книга должна принадлежать к какой-либо категории"
        }
        else if (property == nameof<Book>("publisherID")
            && errorAttribute == ErrorAttributes.notNullMin) {
            msg = "У книги должно быть издательство"
        }

        return msg;
    }

    private getOrdersValidationErrors(property: string, errorAttribute: string): string {
        let msg: string = null;

       if (property == nameof<Order>("name")
            && errorAttribute == ErrorAttributes.required) {
            msg = "Введите ваше имя";
        }
        else if (property == nameof<Order>("name")
            && errorAttribute == ErrorAttributes.pattern) {
            msg = "Имя содержит недопустимые символы";
        }
        else if (property == nameof<Order>("address")
            && errorAttribute == ErrorAttributes.required) {
            msg = "Укажите адрес доставки";
        }
        else if (property == nameof<Order>("address")
            && errorAttribute == ErrorAttributes.pattern) {
            msg = "Неверный формат ввода адреса";
        }
        else if (property == nameof<Payment>("cardNumber")
            && errorAttribute == ErrorAttributes.required) {
            msg = "Неправильный номер банковской карты";
       }
       else if (property == nameof<Payment>("cardNumber")
           && errorAttribute == ErrorAttributes.pattern) {
           msg = "Неправильный номер банковской карты";
       }
        else if (property == nameof<Payment>("cardExpiry")
            && errorAttribute == ErrorAttributes.required) {
            msg = "Неправильный срок окончания банковской карты";
        }
       else if (property == nameof<Payment>("cardExpiry")
           && errorAttribute == ErrorAttributes.pattern) {
           msg = "Неправильный срок окончания банковской карты";
       }
        if (property == nameof<Payment>("cardSecurityCode")
            && errorAttribute == ErrorAttributes.required) {
            msg = "Неправильный код безопасности карты";
        }
        if (property == nameof<Payment>("cardSecurityCode")
            && errorAttribute == ErrorAttributes.pattern) {
            msg = "Неправильный код безопасности карты";
        }

        return msg;
    }
}
