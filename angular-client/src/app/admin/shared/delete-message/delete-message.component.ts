import { Component } from "@angular/core";

import { BsModalRef } from 'ngx-bootstrap/modal';
import { EntityType } from '../../../models/enums/entity-type.enum';

@Component({
    templateUrl: './delete-message.component.html',
})
export class DeleteMessageComponent {
    constructor(
        public bsModalRef: BsModalRef) { }

    entityType: EntityType;
    objectName: string;

    confirmationBtnTitle = "Да";
    cancelationBtnTitle = "Нет";

    result: string = "";

    get formHeader(): string {
        switch (this.entityType) {
            case EntityType.Category:
                return "Удаление категории";
            case EntityType.Publisher:
                return "Удаление издательства";
            case EntityType.Book:
                return "Удаление книги";
            default:
                return "";
        }
    }

    get question(): string {
        switch (this.entityType) {
            case EntityType.Category:
                return "Вы действительно хотите удалить категорию?";
            case EntityType.Publisher:
                return "Вы действительно хотите удалить издательство?";
            case EntityType.Book:
                return "Вы действительно хотите удалить книгу?";
            default:
                return "";
        }
    }

    onDelete(): void {
        this.result = "delete";
        this.bsModalRef.hide();
    }

    onCancel(): void {
        this.result = "cancel";
        this.bsModalRef.hide();
    }
}
