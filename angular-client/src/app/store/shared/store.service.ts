import { Injectable } from "@angular/core";
import { Observable, Subject } from 'rxjs';
import { map } from 'rxjs/operators';

import { QueryOptions } from '../../models/domain/DTO/query-options.model';
import { BookResponse } from '../../models/domain/DTO/book-response.model';
import { Url } from '../../models/url.model';
import { PagedResponse } from '../../models/paged-response.model';
import { RestDatasource } from '../../core/rest-datasource.service';
import { StoreCategoryResponse } from '../../models/domain/DTO/store-category-response.model';
import { Dropdown } from '../../models/domain/DTO/dropdown.model';

@Injectable()
export class StoreService {
    constructor(
        private _rest: RestDatasource) {

        this._queryOptions = new QueryOptions();
        this._queryOptions.resetToDefault();
    }

    private _cardsCountInRow: number = 4;
    private _queryOptions: QueryOptions = null;

    //общее кол-во отображаемых книг на странице
    displayedBooksCount: number
    //кол-во строк отображаемых книг на странице
    rows: Array<number> = new Array<number>();
    cols: Array<number> = new Array<number>();

    //уведомляет об получении новых объектов книг от сервера
    private _booksChanged: Subject<void> = new Subject<void>();
    get booksChanged(): Observable<void> {
        return this._booksChanged.asObservable();
    }

    private _receivedFromServerBooksCount: number = 0;
    get cardsCountInRow(): number {
        return this._cardsCountInRow;
    }

    set cardsCountInRow(value: number) {
        this.setBooksRowsAndCol(value);
    }

    getCategories(): Observable<Array<StoreCategoryResponse>> {
        return this._rest.getAll<Array<StoreCategoryResponse>>(Url.storeCategories);
    }

    getBooks(): Observable<PagedResponse<BookResponse>> {
        return this._rest.receiveAll<PagedResponse<BookResponse>, QueryOptions>(Url.books,
            this._queryOptions)
            .pipe(map(response => {
                this._receivedFromServerBooksCount = response.entities.length;
                this.setBooksRowsAndCol(this._cardsCountInRow);

                return response;
            }));
    }

    getBook(id: number): Observable<BookResponse> {
        return this._rest.getOne<BookResponse>(`${Url.book}/${id}`);
    }

    changeBooksGridSize(cardsCountInRow: number): void {
        this.cardsCountInRow = cardsCountInRow;
    }

    changePage(newPage: number): void {
        this._queryOptions.currentPage = newPage;
        this._booksChanged.next();
    }

    filterBy(filterPropertyName: string, filterPropertyValue: number): void {
        this._queryOptions.currentPage = 1;
        this._queryOptions.filterPropertyName = filterPropertyName;
        this._queryOptions.filterPropertyValue = filterPropertyValue;

        this._booksChanged.next();
    }

    search(options: QueryOptions): void {
        this._queryOptions.currentPage = 1;
        this._queryOptions.searchPropertyNames = options.searchPropertyNames;
        this._queryOptions.searchTerm = options.searchTerm;

        this._booksChanged.next();
    }

    sort(options: QueryOptions): void {
        this._queryOptions.sortPropertyName = options.sortPropertyName;
        this._queryOptions.descendingOrder = options.descendingOrder;

        this._booksChanged.next();
    }

    //св-ва для выпадающих меню
    getDropdownProps(): Observable<Dropdown> {
        return this._rest.getOne<Dropdown>(Url.dropdown);
    }

    private setBooksRowsAndCol(cardsCountInRow: number) {
        this.cols = new Array<number>();
        this.rows = new Array<number>();

        this._cardsCountInRow = cardsCountInRow;
        this.displayedBooksCount = this._receivedFromServerBooksCount;
        for (let i = 0; i < cardsCountInRow; i++)
        {
            this.cols.push(i);
        }
        let row: number = Math.ceil(this.displayedBooksCount / this.cardsCountInRow);
        for (let i = 0; i < row; i++)
        {
            this.rows.push(i);
        }
    }
}
