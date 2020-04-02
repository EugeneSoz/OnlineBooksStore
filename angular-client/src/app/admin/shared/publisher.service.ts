import { Injectable } from "@angular/core";

import { BaseAdminService } from './base-admin.service';
import { Publisher } from '../../models/domain/publisher.model';
import { PublisherDTO } from '../../models/domain/DTO/publisherDTO.model';
import { Url } from '../../models/url.model';
import { RestDatasource } from '../../core/rest-datasource.service';

@Injectable()
export class PublisherService extends BaseAdminService<Publisher, Publisher, PublisherDTO> {

    constructor(
        rest: RestDatasource) {

        super(rest);
        this.getAllUrl = Url.publishers;
        this.getOneUrl = Url.publisher;
        this.createUrl = Url.publisher_create;
        this.updateUrl = Url.publisher_update;
        this.deleteUrl = Url.publisher_delete;
        this.fitlerPropUrl = Url.pub_filter;
        this.sortingPropUrl = Url.pub_sorting;
    }
}
