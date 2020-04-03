import { Injectable } from "@angular/core";
import { Observable } from "rxjs";

import { BaseAdminService } from "./base-admin.service";
import { RestDatasource } from "../../core/rest-datasource.service";
import { BookResponse } from "../../../domain/model/entities/DTO/book-response.model";
import { BookDTO } from "../../../domain/model/entities/DTO/bookDTO.model";
import { Url } from "../../../domain/model/url/url.model";
import { Publisher } from "../../../domain/model/entities/publisher.model";
import { SearchTerm } from "../../../domain/model/entities/DTO/search-term.model";
import { CategoryResponse } from "../../../domain/model/entities/DTO/category-response.model";

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
