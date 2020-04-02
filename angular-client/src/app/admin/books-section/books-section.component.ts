import { Component } from "@angular/core";
import { BookService } from '../shared/book.service';

@Component({
    templateUrl: './books-section.component.html',
    providers: [BookService]
})
export class BooksSectionComponent { }
