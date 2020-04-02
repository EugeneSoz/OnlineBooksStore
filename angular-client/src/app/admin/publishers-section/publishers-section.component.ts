import { Component } from "@angular/core";
import { PublisherService } from '../shared/publisher.service';

@Component({
    templateUrl: './publishers-section.component.html',
    providers: [PublisherService]
})
export class PublishersSectionComponent { }
