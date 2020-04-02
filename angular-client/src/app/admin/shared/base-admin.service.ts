import { Subject } from 'rxjs';

import { QueryOptions } from '../../models/domain/DTO/query-options.model';
import { Pagination } from '../../models/pagination.model';
import { FilterSortingProps } from '../../models/domain/DTO/filter-sorting-props.model';
import { PagedResponse } from '../../models/paged-response.model';
import { RestDatasource } from '../../core/rest-datasource.service';

export class BaseAdminService<TEntity, TEntities, TEntityDTO> {
    constructor(
        protected _rest: RestDatasource) {

        this._queryOptions = new QueryOptions();
        this._queryOptions.resetToDefault();
    }

    protected getOneUrl: string;
    protected getAllUrl: string;
    protected createUrl: string;
    protected updateUrl: string;
    protected deleteUrl: string;
    protected fitlerPropUrl: string;
    protected sortingPropUrl: string;

    entityChanged: Subject<boolean> = new Subject<boolean>();
    entityUpdated: Subject<boolean> = new Subject<boolean>();

    entities: Array<TEntities> = null;
    protected _queryOptions: QueryOptions = null;
    pagination: Pagination = null;
    pageNumbers: Array<number>;
    errors: Array<string> = null;

    sortPropertyName: string;
    searchTerm: string = null;

    filterProps: Array<FilterSortingProps> = null;
    sortingProps: Array<FilterSortingProps> = null;

    private _entity: TEntity = null;
    get entity(): TEntity {
        return this._entity;
    }

    set entity(value: TEntity) {
        this._entity = value;
        let changed: boolean = value == null ? false : true;
        this.entityChanged.next(changed);
    }

    getEntities(): void {
        this._rest.receiveAll<PagedResponse<TEntities>, QueryOptions>(this.getAllUrl, this._queryOptions)
            .subscribe(result => {
                this.entities = result.entities;
                this.pagination = result.pagination;
                this.pageNumbers = result.pageNumbers;
            });
    }

    getEntity(id: number): void {
        this._rest.getOne<TEntity>(`${this.getOneUrl}/${id}`)
            .subscribe(result => {
                this.entity = result;
            });
    }

    getFilterProps(): void {
        this._rest.getAll<FilterSortingProps[]>(this.fitlerPropUrl)
            .subscribe(result => this.filterProps = result);
    }

    getSortingProps(): void {
        this._rest.getAll<FilterSortingProps[]>(this.sortingPropUrl)
            .subscribe(result => this.sortingProps = result);
    }

    createEntity(model: TEntityDTO): void {
        this._rest.create<TEntityDTO>(`${this.createUrl}`, model)
            .subscribe((result: boolean) => {},
            (errors) => this.errors = <string[]>errors);
    }

    updateEntity(model: TEntityDTO): void {
        this._rest.update<TEntityDTO>(`${this.updateUrl}`, model)
            .subscribe((result: boolean) => {
                this.entityUpdated.next(result);
            },
            (errors) => this.errors = <string[]>errors);
    }

    deleteEntity(model: TEntityDTO): void {
        this._rest.delete<TEntityDTO>(this.deleteUrl, model)
            .subscribe((result: boolean) => {
                if (result) {
                    this.entity = null;
                    this.getEntities();
                }
            });
    }

    search(options: QueryOptions): void {
        this._queryOptions.currentPage = 1;
        this._queryOptions.searchPropertyNames = options.searchPropertyNames;
        this._queryOptions.searchTerm = options.searchTerm;
        this.searchTerm = options.searchTerm;

        this.getEntities();
    }

    sort(options: QueryOptions): void {
        this._queryOptions.sortPropertyName = options.sortPropertyName;
        this._queryOptions.descendingOrder = options.descendingOrder;
        this.sortPropertyName = options.sortPropertyName;

        this.getEntities();
    }

    changePage(newPage: number): void {
        this._queryOptions.currentPage = newPage;
        this.getEntities();
    }

    resetQueryOptionsToDefault(): void {
        this._queryOptions.resetToDefault();
    }
}
