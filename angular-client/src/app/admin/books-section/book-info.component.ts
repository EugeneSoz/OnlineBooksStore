import { Component, Input } from "@angular/core";

import { BookResponse } from "../../../domain/model/entities/DTO/book-response.model";

@Component({
    selector: "bs-book-info",
    templateUrl: "./book-info.component.html",
})
export class BookInfoComponent {
    @Input() book: BookResponse = null;
}
