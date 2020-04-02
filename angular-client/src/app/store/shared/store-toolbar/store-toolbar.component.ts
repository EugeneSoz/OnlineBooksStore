import { Component, Input } from '@angular/core';

@Component({
    selector: 'bs-store-toolbar',
    templateUrl: './store-toolbar.component.html',
})
export class StoreToolbarComponent {
    @Input() isToolbarActionsVisible: boolean = true;
    @Input() isSearchToolbarVisible: boolean = true;
    isCollapsed: boolean = true;
}
