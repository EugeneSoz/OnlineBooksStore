import { Component, OnInit } from '@angular/core';

import { StoreService } from '../../shared/store.service';
import { BooksGridType } from '../../../models/enums/book-grid-type.enum';
import { QueryOptions } from '../../../models/domain/DTO/query-options.model';
import { ListItem } from '../../../models/domain/DTO/dropdown.model';

@Component({
    selector: '.navbar-nav .mr-auto',
    templateUrl: './actions.component.html',
})
export class ActionsComponent implements OnInit {

    constructor(
        private _storeService: StoreService) { }

    gridSizeProperties: Array<ListItem>;
    sortingProperties: Array<ListItem>;
    gridSizeName: string = BooksGridType.ThreeByFour;

    private _sortPropertyName: string = "";

    private _descendingOrder: boolean = false;

    ngOnInit() {
        this.getDropdownProps();
    }

    private getDropdownProps(): void {
        this._storeService.getDropdownProps().subscribe((result) => {
            this.gridSizeProperties = result.gridSizeProperties;
            this.sortingProperties = result.sortingProperties;
        });
    }
    //выделить в выпадающем списке свойство
    setElementCssClass(listItem: ListItem): string
    {
        let cssClass: string = "";

        if (listItem.propertyName == this.gridSizeName) {
            cssClass = " active";
        }

        if (listItem.propertyName == this._sortPropertyName
            && listItem.descendingOrder == this._descendingOrder) {
            cssClass = " active";
        }

        return `dropdown-item${cssClass}`;
    }

    onChangeBooksGridSize(listItem: ListItem): void
    {
        let cardsCountInRow: number = 0;
        switch (listItem.propertyName) {
            case BooksGridType.SixByTwo:
                cardsCountInRow = 2;
                break;
            case BooksGridType.FourByThree:
                cardsCountInRow = 3;
                break;
            default:
                cardsCountInRow = 4;
                break;
        }

        this.gridSizeName = listItem.propertyName;
        this._storeService.changeBooksGridSize(cardsCountInRow);
    }

    onSort(listItem: ListItem): void {
        this._descendingOrder = listItem.descendingOrder;
        let options: QueryOptions = new QueryOptions();
        this._sortPropertyName = listItem.propertyName;
        options.sortPropertyName = listItem.propertyName;
        options.descendingOrder = listItem.descendingOrder;

        this._storeService.sort(options);
    }
}
