import { Component, OnInit } from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";

import { PublisherService } from "../shared/publisher.service";
import { BaseAdminFormComponent } from "../../../domain/model/components/base-admin-form.model";
import { PublisherFormGroup } from "../../../domain/model/forms/publisher-form.model";
import { PageLink } from "../../../domain/model/url/page-link.model";
import { PublisherDTO } from "../../../domain/model/entities/DTO/publisherDTO.model";
import { Book } from "../../../domain/model/entities/book.model";
import { createPageLink } from "../../../infrastructure/helper-functions";

@Component({
    templateUrl: "./publisher-form.component.html",
})
export class PublisherFormComponent extends BaseAdminFormComponent<PublisherFormGroup> implements OnInit {
    constructor(
        private _publisherService: PublisherService,
        activeRoute: ActivatedRoute,
        private _router: Router) {

        super(activeRoute);
        this.form = new PublisherFormGroup(this.publisher);
        this.pageLink = createPageLink(true, PageLink.admin, PageLink.publishers);
    }

    publisher: PublisherDTO = new PublisherDTO();
    books: Array<Book> = null;

    get errors(): Array<string> {
        return this._publisherService.errors;
    }

    ngOnInit(): void {
        this._subscriptions.push(
            this._publisherService.entityChanged.subscribe(changed => {
                if (changed) {
                    this.books = this._publisherService.entity.books;
                    this.publisher = this._ee.mapPublisherDTO(this._publisherService.entity);
                    this.form = new PublisherFormGroup(this.publisher);
                }
            })
        );
        this._subscriptions.push(
            this._publisherService.entityUpdated.subscribe(updated => {
                if (updated) {
                    this._router.navigateByUrl(this.pageLink);
                }
            })
        );

        if (this._id != 0) {
            this._publisherService.getEntity(this._id);
        }
    }

    onSubmit(): void {
        if (!this.form.valid) {
            return;
        }

        this.publisher = this.form.value;
        //update
        if (this.editing) {
            this._publisherService.updateEntity(this.publisher);
        }//create
        else {
            this._publisherService.createEntity(this.publisher)
            this.isAlertVisible = true;
            this.publisher = new PublisherDTO();
        }

        this.form.reset();
    }

    ngOnDestroy(): void {
        super.ngOnDestroy();
        this._publisherService.entity = null;
    }
}
