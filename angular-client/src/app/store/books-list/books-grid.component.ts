import { Component, OnInit, OnDestroy } from '@angular/core';
import { Subscription } from 'rxjs';

import { StoreService } from '../shared/store.service';
import { BookResponse } from '../../models/domain/DTO/book-response.model';
import { Pagination } from '../../models/pagination.model';
import { PagedResponse } from '../../models/paged-response.model';

@Component({
    selector: '[id=store-main-content]',
    templateUrl: './books-grid.component.html',
})
export class BooksGridComponent implements OnInit, OnDestroy
{
    constructor(
        private _storeService: StoreService) {}

    private _subscriptions: Array<Subscription> = new Array<Subscription>();
    books: Array<BookResponse> = null;
    pagination: Pagination = null;
    pageNumbers: Array<number> = null;

    //кол-во книжных карт в строке
    get cardsCountInRow(): number {
        return this._storeService.cardsCountInRow;
    }

    //общее кол-во отображаемых книг на странице
    get displayedBooksCount(): number  {
        return this._storeService.displayedBooksCount;
    }

    get rows(): Array<number> {
        return this._storeService.rows;
    }

    get cols(): Array<number> {
        return this._storeService.cols;
    }

    ngOnInit(): void {
        this._subscriptions.push(this._storeService.booksChanged.subscribe(() => this.getBooks()));
        this.getBooks();
    }

    private getBooks(): void {
        this._storeService.getBooks()
            .subscribe((response: PagedResponse<BookResponse>) => {
                this.books = response.entities;
                this.pagination = response.pagination;
                this.pageNumbers = response.pageNumbers;
            });
    }

    //получить книгу из массива исходя из номера строки и столбца
    getBookIndex(row: number, column: number): number {
        return this.cardsCountInRow * row + column;
    }

    isColEmpty(row: number, column: number): boolean {
        return this.getBookIndex(row, column) < this.displayedBooksCount
            ? false
            : true;
    }

    onChangePage(newPage: number): void {
        this._storeService.changePage(newPage);
    }

    ngOnDestroy(): void {
        this._subscriptions.forEach(subscription => subscription.unsubscribe());
    }
}
