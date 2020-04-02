import { Component, Input } from '@angular/core';
import { Book } from '../../../models/domain/book.model';
import { createPageLink } from '../../../core/helper-functions';
import { PageLink } from '../../../models/enums/page-link.enum';

@Component({
    selector: 'bs-related-books-table',
    templateUrl: './related-books-table.component.html',
})
export class RelatedBookTableComponent {
    @Input() books: Array<Book> = null;

    constructor() {
        this.goToBookPageLink = createPageLink(true, PageLink.admin, PageLink.books);
    }
    goToBookPageLink: string;
}
