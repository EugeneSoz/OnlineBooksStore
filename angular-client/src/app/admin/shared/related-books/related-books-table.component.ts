import { Component, Input } from "@angular/core";
import { Book } from "../../../../domain/model/entities/book.model";
import { PageLink } from "../../../../domain/model/url/page-link.model";
import { createPageLink } from "../../../../infrastructure/helper-functions";

@Component({
    selector: "bs-related-books-table",
    templateUrl: "./related-books-table.component.html",
})
export class RelatedBookTableComponent {
    @Input() books: Array<Book> = null;

    constructor() {
        this.goToBookPageLink = createPageLink(true, PageLink.admin, PageLink.books);
    }
    goToBookPageLink: string;
}
