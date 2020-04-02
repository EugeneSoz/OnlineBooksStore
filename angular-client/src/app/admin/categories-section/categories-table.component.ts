import { Component, OnInit, OnDestroy } from '@angular/core';
import { Subscription } from 'rxjs';

import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { BaseTableComponent } from '../../models/components/base-table.model';
import { Category } from '../../models/domain/category.model';
import { CategoryResponse } from '../../models/domain/DTO/category-response.model';
import { CategoryDTO } from '../../models/domain/DTO/categoryDTO.model';
import { CategoryService } from '../shared/category.service';
import { EntityType } from '../../models/enums/entity-type.enum';
import { PageLink } from '../../models/enums/page-link.enum';
import { EntityExtensions } from '../../models/entity-extensions.model';
import { DeleteMessageComponent } from '../shared/delete-message/delete-message.component';
import { createPageLink } from '../../core/helper-functions';

@Component({
    templateUrl: './categories-table.component.html',
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
                    (this.modalRef.content as DeleteMessageComponent).result == "delete") {
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
