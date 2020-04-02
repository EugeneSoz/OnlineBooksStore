import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';

import { StoreService } from './shared/store.service';
import { PageLink } from '../models/enums/page-link.enum';
import { nameof } from '../core/helper-functions';
import { BookResponse } from '../models/domain/DTO/book-response.model';
import { StoreCategoryResponse } from '../models/domain/DTO/store-category-response.model';

@Component({
    selector: 'bs-store-sidebar',
    templateUrl: './store-sidebar.component.html',
})
export class StoreSidebarComponent implements OnInit {

    constructor(
        private _storeService: StoreService) {
        this.pageLink = `/${PageLink.admin}`;
    }

    pageLink: string;
    private _selectedCategoryId: number = 0;
    private _selectedSubCategoryId: number = 0;

    categories: Array<StoreCategoryResponse>;

    ngOnInit(): void {
        this.getCategories();
    }

    private getCategories(): void {
        this._storeService.getCategories()
            .subscribe(result => this.categories = result)
    }

    onFilter(category: StoreCategoryResponse = null): void {
        let filterPropertyName: string = "";
        if (category == null) {
            this._selectedCategoryId = 0;
            this._selectedSubCategoryId = 0;
            category = new StoreCategoryResponse();
        }
        else if (category.isParent) {
            this._selectedCategoryId = category.id;
            this._selectedSubCategoryId = 0;
            filterPropertyName = nameof<BookResponse>("categoryName");
        }
        else {
            this._selectedCategoryId = category.parentCategoryID;
            this._selectedSubCategoryId = category.id;
            filterPropertyName = nameof<BookResponse>("subcategoryName");
        }

        category.isCollapsed = !category.isCollapsed;
        this._storeService.filterBy(filterPropertyName, category.id);
    }

    checkOnSelection(category: StoreCategoryResponse): boolean {
        if (category.id == this._selectedCategoryId || category.id == this._selectedSubCategoryId) {
            return true;
        }
        else {
            return false;
        }
    }
}
