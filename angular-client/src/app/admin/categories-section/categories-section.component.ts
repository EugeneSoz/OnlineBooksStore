import { Component } from '@angular/core';
import { CategoryService } from '../shared/category.service';

@Component({
    templateUrl: './categories-section.component.html',
    providers: [CategoryService]
})
export class CategoriesSectionComponent {
}
