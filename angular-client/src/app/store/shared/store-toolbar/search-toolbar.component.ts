import { Component } from '@angular/core';

import { StoreService } from '../../shared/store.service';
import { QueryOptions } from '../../../models/domain/DTO/query-options.model';
import { BookResponse } from '../../../models/domain/DTO/book-response.model';
import { nameof } from '../../../core/helper-functions';

@Component({
    selector: 'bs-search-toolbar',
    templateUrl: './search-toolbar.component.html',
})
export class SearchToolbarComponent {
    constructor(
        private _storeService: StoreService) { }

    searchTerm: string = null;

    get isClearBtnVisible(): boolean {
        return this.searchTerm == null ? false : true;
    }

    onInputSearchValue(value: string): void {
        this.searchTerm = value;
    }

    onSearch(clear: boolean = false): void {
        let options: QueryOptions = new QueryOptions();
        if (clear) {
            this.searchTerm = null;
            options.searchPropertyNames = null;
            options.searchTerm = null;
        }
        else {
            options.searchPropertyNames = new Array(
                nameof<BookResponse>("title"),
                nameof<BookResponse>("authors"));

            options.searchTerm = this.searchTerm;
        }

        this._storeService.search(options);
    }

    onClear(): void {
        this.searchTerm = null;
        this.onSearch(true);
    }
}
