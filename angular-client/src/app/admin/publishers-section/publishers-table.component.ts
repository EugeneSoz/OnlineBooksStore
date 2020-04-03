import { Component, OnInit, OnDestroy } from "@angular/core";
import { Subscription } from "rxjs";

import { BsModalService, BsModalRef } from "ngx-bootstrap/modal";
import { PublisherService } from "../shared/publisher.service";
import { DeleteMessageComponent } from "../shared/delete-message/delete-message.component";
import { BaseTableComponent } from "../../../domain/model/components/base-table.model";
import { Publisher } from "../../../domain/model/entities/publisher.model";
import { PublisherDTO } from "../../../domain/model/entities/DTO/publisherDTO.model";
import { EntityType } from "../../../domain/model/enums/entity-type.enum";
import { PageLink } from "../../../domain/model/url/page-link.model";
import { EntityExtensions } from "../../../domain/model/entity-extensions.model";
import { createPageLink } from "../../../infrastructure/helper-functions";

@Component({
    templateUrl: "./publishers-table.component.html",
})
export class PublishersTableComponent extends BaseTableComponent<Publisher, Publisher, PublisherDTO>
    implements OnInit, OnDestroy {

    constructor(
        publisherService: PublisherService,
        private modalService: BsModalService) {

        super(
            publisherService,
            EntityType.Publisher,
            modalService,
            createPageLink(true, PageLink.admin, PageLink.publishers));
    }

    publisherDTO: PublisherDTO = null;
    modalRef: BsModalRef = null;

    onShowDeleteModal(publisher: Publisher): void {
        let ee: EntityExtensions = new EntityExtensions();
        this.publisherDTO = ee.mapPublisherDTO(publisher);
        const initialState = {
            entityType: EntityType.Publisher,
            objectName: publisher.name
        }

        this._subscriptions.push(
            this.modalService.onHide.subscribe(() => {
                if (this.modalRef != null &&
                    (this.modalRef.content as DeleteMessageComponent).result == "delete") {
                    this._service.deleteEntity(this.publisherDTO);
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
