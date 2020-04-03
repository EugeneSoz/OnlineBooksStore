import { Injectable } from "@angular/core";
import { BaseAdminService } from "./base-admin.service";
import { RestDatasource } from "../../core/rest-datasource.service";
import { Category } from "../../../domain/model/entities/category.model";
import { CategoryResponse } from "../../../domain/model/entities/DTO/category-response.model";
import { CategoryDTO } from "../../../domain/model/entities/DTO/categoryDTO.model";
import { Url } from "../../../domain/model/url/url.model";

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
