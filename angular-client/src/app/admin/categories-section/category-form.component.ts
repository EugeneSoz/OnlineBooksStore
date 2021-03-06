import { Component, OnInit } from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";

import { CategoryService } from "../shared/category.service";
import { BaseAdminFormComponent } from "../../../domain/model/components/base-admin-form.model";
import { CategoryFormGroup } from "../../../domain/model/forms/category-form.model";
import { PageLink } from "../../../domain/model/url/page-link.model";
import { CategoryDTO } from "../../../domain/model/entities/DTO/categoryDTO.model";
import { Book } from "../../../domain/model/entities/book.model";
import { Category } from "../../../domain/model/entities/category.model";
import { createPageLink } from "../../../infrastructure/helper-functions";

@Component({
    templateUrl: "./category-form.component.html",
})
export class CategoryFormComponent extends BaseAdminFormComponent<CategoryFormGroup> implements OnInit {
    constructor(
        private _categoryService: CategoryService,
        activeRoute: ActivatedRoute,
        private _router: Router) {

        super(activeRoute);
        this.form = new CategoryFormGroup(this.category);
        this.pageLink = createPageLink(true, PageLink.admin, PageLink.categories);
    }

    category: CategoryDTO = new CategoryDTO();
    books: Array<Book> = null;

    get errors(): Array<string> {
        return this._categoryService.errors;
    }

    get parentCategories(): Array<Category> {
        return this._categoryService.parentCategories;
    }

    ngOnInit(): void {
        this._subscriptions.push(
            this._categoryService.entityChanged.subscribe(changed => {
                if (changed) {
                    this.books = this._categoryService.entity.books;
                    this.category = this._ee.mapCategoryDTO(this._categoryService.entity);
                    this.form = new CategoryFormGroup(this.category);
                }
            })
        );
        this._subscriptions.push(
            this._categoryService.entityUpdated.subscribe(updated => {
                if (updated) {
                    this._router.navigateByUrl(this.pageLink);
                }
            })
        );

        if (this._id != 0) {
            this._categoryService.getEntity(this._id);
        }

        this._categoryService.getParentCategories();
    }

    onSubmit(): void {
        if (!this.form.valid) {
            return;
        }

        this.category = this.form.value;
        if (this.category.parentCategoryID == 0) {
            this.category.parentCategoryID = null;
        }
        //update
        if (this.editing) {
            this._categoryService.updateEntity(this.category);
        }//create
        else {
            this._categoryService.createEntity(this.category);
            this.isAlertVisible = true;
            this.category = new CategoryDTO();
            this._categoryService.getParentCategories();
        }

        this.form.reset();
    }

    ngOnDestroy(): void {
        super.ngOnDestroy();
        this._categoryService.entity = null;
        this._categoryService.parentCategories = null;
    }
}
