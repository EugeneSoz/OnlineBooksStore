import { Component, Input, OnChanges, SimpleChanges, Output, EventEmitter } from "@angular/core";

import { FilterSortingProps } from '../../../models/domain/DTO/filter-sorting-props.model';
import { QueryOptions } from '../../../models/domain/DTO/query-options.model';

@Component({
    selector: 'bs-admin-filter',
    templateUrl: './admin-filter.component.html',
})
export class AdminFilterComponent implements OnChanges {
    @Input() properties: Array<FilterSortingProps> = null;
    @Input() term: string = null;
    @Output() searchEvent = new EventEmitter<QueryOptions>();

    searchProperyName: string;
    searchTerm: string = null;

    get isClearBtnVisible(): boolean {
        return this.term == null ? false : true;
    }

    ngOnChanges(changes: SimpleChanges): void {
        let props = changes["properties"];
        if (props != null && props.currentValue != props.previousValue) {
            this.searchProperyName = this.properties[0].propertyName;
        }
    }

    onSelectProperty(property: string): void {
        this.searchProperyName = property;
    }

    onInputSearchValue(value: string): void {
        this.searchTerm = value;
    }

    onSearch(clear: boolean = false): void {
        let options: QueryOptions = new QueryOptions();
        if (clear) {
            options.searchPropertyNames = null;
            options.searchTerm = null;
        }
        else {
            options.searchPropertyNames = new Array<string>(this.searchProperyName);
            options.searchTerm = this.searchTerm;
        }

        this.searchEvent.emit(options);
    }

    onClear(): void {
        this.searchTerm = null;
        this.onSearch(true);
    }
}
