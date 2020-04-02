import { Injectable } from '@angular/core';
import { BaseAdminService } from './base-admin.service';
import { Category } from '../../models/domain/category.model';
import { CategoryResponse } from '../../models/domain/DTO/category-response.model';
import { CategoryDTO } from '../../models/domain/DTO/categoryDTO.model';
import { Url } from '../../models/url.model';
import { RestDatasource } from '../../core/rest-datasource.service';

@Injectable()
export class CategoryService extends BaseAdminService<Category, CategoryResponse, CategoryDTO> {

    constructor(
        rest: RestDatasource) {

        super(rest);
        this.getAllUrl = Url.categories;
        this.getOneUrl = Url.category;
        this.createUrl = Url.category_create;
        this.updateUrl = Url.category_update;
        this.deleteUrl = Url.category_delete;
        this.fitlerPropUrl = Url.cat_filter;
        this.sortingPropUrl = Url.cat_sorting;
        this.url = Url.parentCategories;
    }

    url: string = "";
    parentCategories: Array<Category> = null;

    getParentCategories(): void {
        this._rest.getAll<Array<Category>>(this.url)
            .subscribe(result => {
                this.parentCategories = result;
            });
    }
}
