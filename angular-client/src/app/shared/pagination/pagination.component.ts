import { Component, Input, EventEmitter, Output, OnChanges, SimpleChanges } from '@angular/core';

import { Pagination } from '../../models/pagination.model';
import { PaginationButtonType } from '../../models/enums/pagination-button-type.enum';

@Component({
    selector: 'bs-pagination',
    templateUrl: './pagination.component.html',
})
export class PaginationComponent implements OnChanges {

    @Input() pagination: Pagination;
    @Input() pageNumbers: Array<number>;
    @Output() pageNumberEvent = new EventEmitter<number>();

    selectedPage: number = 1;
    first: number = PaginationButtonType.First;
    last: number = PaginationButtonType.Last;
    previous: number = PaginationButtonType.Previous;
    next: number = PaginationButtonType.Next;

    ngOnChanges(changes: SimpleChanges) {
        let chng = changes["pagination"];
        if (chng.currentValue != chng.previousValue) {
            this.selectedPage = this.pagination.currentPage;
        }
    }

    onChangePage(newPage: number): void {
        if (newPage == this.previous && this.pagination.hasPreviousPage == false ||
            newPage == this.next && this.pagination.hasNextPage == false) {
            return;
        }

        if (newPage == this.first && this.selectedPage == 1 ||
            newPage == this.last && this.selectedPage == this.pagination.totalPages) {
            return;
        }

        switch (newPage) {
            case PaginationButtonType.First:
                this.selectedPage = 1;
                break;
            case PaginationButtonType.Last:
                this.selectedPage = this.pagination.totalPages;
                break;
            case PaginationButtonType.Previous:
                this.selectedPage = this.pagination.currentPage - 1;
                break;
            case PaginationButtonType.Next:
                this.selectedPage = this.pagination.currentPage + 1;
                break;
        }

        if (newPage > 0 && newPage <= this.pagination.totalPages) {
            this.selectedPage = newPage;
        }

        this.pageNumberEvent.emit(this.selectedPage);
    }
}
