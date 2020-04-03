import { Component, OnInit, OnDestroy } from "@angular/core";
import { Subscription } from "rxjs";

import { BsModalService, BsModalRef } from "ngx-bootstrap/modal";
import { CategoryService } from "../shared/category.service";
import { DeleteMessageComponent } from "../shared/delete-message/delete-message.component";
import { BaseTableComponent } from "../../../domain/model/components/base-table.model";
import { Category } from "../../../domain/model/entities/category.model";
import { CategoryResponse } from "../../../domain/model/entities/DTO/category-response.model";
import { CategoryDTO } from "../../../domain/model/entities/DTO/categoryDTO.model";
import { EntityType } from "../../../domain/model/enums/entity-type.enum";
import { PageLink } from "../../../domain/model/url/page-link.model";
import { EntityExtensions } from "../../../domain/model/entity-extensions.model";
import { createPageLink } from "../../../infrastructure/helper-functions";

@Component({
    templateUrl: "./categories-table.component.html",
})
export class CategoriesTableComponent extends BaseTableComponent<Category, CategoryResponse, CategoryDTO>
    implements OnInit, OnDestroy {

    constructor(
        categoryService: CategoryService,
        private modalService: BsModalService) {

        super(
            categoryService,
            EntityType.Category,
            modalService,
            createPageLink(true, PageLink.admin, PageLink.categories));
    }

    categoryDTO: CategoryDTO = null;
    modalRef: BsModalRef = null;

    onShowDeleteModal(category: Category): void {
        let ee: EntityExtensions = new EntityExtensions();
        this.categoryDTO = ee.mapCategoryDTO(category);
        const initialState = {
            entityType: EntityType.Category,
            objectName: category.displayedName
        }

        this._subscriptions.push(
            this.modalService.onHide.subscribe(() => {
                if (this.modalRef != null &&
                    (this.modalRef.content as DeleteMessageComponent).result === "delete") {
                    this._service.deleteEntity(this.categoryDTO);
                }
                this.unsubscribe();
            })
        );

        this.modalRef = this.modalService.show(DeleteMessageComponent, { initialState });
    }

    unsubscribe(): void {
        this._subscriptions.forEach((subscription: Subscription) => {
            subscription.unsubscribe();
        });
        this._subscriptions = new Array<Subscription>();
    }
}
