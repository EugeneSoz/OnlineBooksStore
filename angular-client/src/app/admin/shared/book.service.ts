import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { Url } from '../../models/url.model';
import { BaseAdminService } from './base-admin.service';
import { BookResponse } from '../../models/domain/DTO/book-response.model';
import { BookDTO } from '../../models/domain/DTO/bookDTO.model';
import { SearchTerm } from '../../models/domain/DTO/search-term.model';
import { Publisher } from '../../models/domain/publisher.model';
import { CategoryResponse } from '../../models/domain/DTO/category-response.model';
import { RestDatasource } from '../../core/rest-datasource.service';

@Injectable()
export class BookService extends BaseAdminService<BookResponse, BookResponse, BookDTO> {

    constructor(
        rest: RestDatasource) {

        super(rest);
        this.getAllUrl = Url.books;
        this.getOneUrl = Url.book;
        this.createUrl = Url.book_create;
        this.updateUrl = Url.book_update;
        this.deleteUrl = Url.book_delete;
        this.fitlerPropUrl = Url.book_filter;
        this.sortingPropUrl = Url.book_sorting;
        this.publishersUrl = Url.publishersForSelection;
        this.categoriesUrl = Url.categoriesForSelection;
    }

    //для получения с сервера значений для выбора при создании (или редактировании) книги
    private publishersUrl: string = null;
    private categoriesUrl: string = null;

    searchSelectablePublishers(searchTerm: string): Observable<Array<Publisher>> {
        let term: SearchTerm = new SearchTerm(searchTerm);

        return this._rest.receiveAll<Array<Publisher>, SearchTerm>(this.publishersUrl, term);
    }

    searchSelectableCategories(searchTerm: string): Observable<Array<CategoryResponse>> {
        let term: SearchTerm = new SearchTerm(searchTerm);

        return this._rest.receiveAll<Array<CategoryResponse>, SearchTerm>(this.categoriesUrl, term);
    }
}
