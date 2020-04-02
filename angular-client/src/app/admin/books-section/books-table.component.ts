import { Component, OnInit, OnDestroy } from '@angular/core';
import { Subscription } from 'rxjs';

import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { BaseTableComponent } from '../../models/components/base-table.model';
import { BookResponse } from '../../models/domain/DTO/book-response.model';
import { BookDTO } from '../../models/domain/DTO/bookDTO.model';
import { BookService } from '../shared/book.service';
import { EntityType } from '../../models/enums/entity-type.enum';
import { PageLink } from '../../models/enums/page-link.enum';
import { EntityExtensions } from '../../models/entity-extensions.model';
import { DeleteMessageComponent } from '../shared/delete-message/delete-message.component';
import { createPageLink } from '../../core/helper-functions';

@Component({
    templateUrl: './books-table.component.html',
})
export class BooksTableComponent extends BaseTableComponent<BookResponse, BookResponse, BookDTO>
    implements OnInit, OnDestroy {

    constructor(
        bookService: BookService,
        private modalService: BsModalService) {

        super(
            bookService,
            EntityType.Book,
            modalService,
            createPageLink(true, PageLink.admin, PageLink.books));
    }

    bookDTO: BookDTO = null;
    modalRef: BsModalRef = null;

    onShowDeleteModal(book: BookResponse): void {
        let ee: EntityExtensions = new EntityExtensions();
        this.bookDTO = ee.mapBookDTO(book);
        const initialState = {
            entityType: EntityType.Book,
            objectName: book.title
        }

        this._subscriptions.push(
            this.modalService.onHide.subscribe(() => {
                if (this.modalRef != null &&
                    (this.modalRef.content as DeleteMessageComponent).result == "delete") {
                    this._service.deleteEntity(this.bookDTO);
                }
                this.unsubscribe();
            })
        );

        this.modalRef = this.modalService.show(DeleteMessageComponent, { initialState });
    }

    unsubscribe(): void {
        this._subscriptions.forEach((subscription: Subscription) => {
            subscription.unsubscribe();
        });
        this._subscriptions = new Array<Subscription>();
    }
}
